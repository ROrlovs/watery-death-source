using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    public healthbar healthBar;
    
    private bool playerDead;

    private GameObject playerSpriteObj;

    private float level;

    SoundEffectScript sfxScript;

    private float strength;
    GameObject leveluptext;


    // Start is called before the first frame update
    void Start()
    {
        playerSpriteObj = GameObject.Find("PlayerSprite");
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
        sfxScript = GameObject.FindGameObjectWithTag("PlayerAudioSource").GetComponent<SoundEffectScript>();
        leveluptext = GameObject.FindGameObjectWithTag("LevelUpText");
        leveluptext.SetActive(false);
    }

    public void TakeDamage(float amount){
        if(!playerDead){
            currentHealth -=amount;
            StartCoroutine(TakeDamageColor());
            healthBar.SetSlider(currentHealth);
            sfxScript.PlaySound("player_hit");
        }
        else{
            //Debug.Log("Cannot take damage. Player is already dead.");
        }
        
    }

    public void IncrementLevel(){
        level++;
        maxHealth = maxHealth + level*15;
        strength = strength + level/2;
        GameObject.FindGameObjectWithTag("StatsText").GetComponent<TextMeshProUGUI>().text = "STRENGTH: "+strength+" CRIT: "+(20+strength/2)+"%"+" MAX HEALTH: "+maxHealth;
        GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>().text = "LEVEL: "+level;
        StartCoroutine(LevelUpTextShow());
    }

    private IEnumerator LevelUpTextShow(){
        leveluptext.SetActive(true);
        yield return new WaitForSeconds(2);
        leveluptext.SetActive(false);
    }

    public float GetStrength(){
        return strength;
    }

    public void RestoreHealth(float amount){
        if(!playerDead){
            currentHealth +=amount;
            StartCoroutine(RestoreHealthColor());
            healthBar.SetSlider(currentHealth);
        }

        
    }

    void Update()
    {
        if (currentHealth>maxHealth){
            currentHealth = maxHealth;
        }

        if(currentHealth <=0 && !playerDead){
            Die();
        }

        if(playerDead){
            GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = Color.gray;
        }


        
    }

    private IEnumerator TakeDamageColor(){
        playerSpriteObj.GetComponent<SpriteRenderer>().color=Color.red;
        yield return new WaitForSeconds(0.2f);
        playerSpriteObj.GetComponent<SpriteRenderer>().color=Color.white;
    }

    private IEnumerator RestoreHealthColor(){
        playerSpriteObj.GetComponent<SpriteRenderer>().color=Color.green;
        yield return new WaitForSeconds(0.2f);
        playerSpriteObj.GetComponent<SpriteRenderer>().color=Color.white;
    }

    public void Die(){
        //Debug.Log("die");
        playerDead=true;
        sfxScript.PlaySound("player_death");
        GameObject.Find("TimeManager").GetComponent<TimeManagerScript>().BulletTimeActivate();
        playerDead = true;
        GameObject.FindGameObjectWithTag("PlayerAudioSource").GetComponent<AudioSource>().Stop();
        GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = Color.gray;
        StartCoroutine(DeathAnim());
    }

    private IEnumerator DeathAnim(){

        yield return new WaitForSecondsRealtime(4);
        GameObject.Find("TimeManager").GetComponent<TimeManagerScript>().BulletTimeActivate();
        SceneManager.LoadScene(2);
    }


}
