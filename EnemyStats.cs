using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;
    [SerializeField] private float speed;

    private float originalSpeed;

    public healthbar healthBar;
    
    private bool playerDead;

    AudioSource audioSource;

    EnemySoundScript sfxScript;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
        originalSpeed=speed;
        sfxScript = GetComponentInChildren<EnemySoundScript>();
        maxHealth = 100+(GameObject.Find("WaveManager").GetComponent<WaveManagerScript>().GetCurrentWave()*10);
    }

    public void TakeDamage(float amount){
        if(!playerDead){
            this.currentHealth -=amount;
            Debug.Log(this + " took " + amount + " damage");
            StartCoroutine(TakeDamageColor());
            this.healthBar.SetSlider(currentHealth);
            sfxScript.PlaySound("enemy_hit");
        }
        else{
            //Debug.Log("Cannot take damage. Enemy is already dead.");
        }
        
    }

    private IEnumerator TakeDamageColor(){
        if(!GetComponent<EnemyMovement>().slowed){

            GetComponent<SpriteRenderer>().color=Color.red;
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color=Color.white;

        }
        
    }

    public void ChangeSpeed(float new_speed){
        float originalSpeed;
        originalSpeed = speed;
        speed = new_speed;
    }

    public float GetOriginalSpeed(){
        return originalSpeed;
    }

    public void ResetSpeed(){
        speed = originalSpeed;
    }

    public float GetSpeed(){
        return speed;
    }


    void Update()
    {
        if (currentHealth>maxHealth){
            currentHealth = maxHealth;
        }

        if(currentHealth <=0 && !playerDead){
            Die();
        }
        
    }

    public void Die(){
        Debug.Log("die");
        GameObject.Find("EnemyDropsManager").GetComponent<EnemyDropsManagerScript>().DropItem(30,this.gameObject);
        playerDead = true;
        sfxScript.PlaySound("skeleton_death");
        GameObject.Find("WaveManager").GetComponent<WaveManagerScript>().RemoveEnemy(this.gameObject);
        GameObject.Find("WaveManager").GetComponent<WaveManagerScript>().IncrementEnemiesKilled();
        Destroy(this.gameObject);
    }

    public EnemySoundScript GetSFXScript(){
        return sfxScript;
    }
}
