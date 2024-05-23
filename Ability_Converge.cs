using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Ability_Converge : MonoBehaviour
{

    private bool colliding;
    private int timer;
    private int windupTimer;

    public float cooldown;

    private GameObject enemy;

    private List<GameObject> enemyArray = new List<GameObject>();

    [SerializeField] GameObject leftwave;
    [SerializeField] GameObject rightwave;

    SoundEffectScript sfxScript;



    void Start(){
        timer=0;
        windupTimer=1000;
        sfxScript = GameObject.FindGameObjectWithTag("PlayerAudioSource").GetComponent<SoundEffectScript>();
        sfxScript.PlaySound("converge_windup");
        StartCoroutine(ConvergeFire());
        
        
    }

   





    private IEnumerator ConvergeFire(){

        
        //Debug.Log("frozen breath coroutune");
    
        
        yield return new WaitForSeconds(1);
        

        
        
        if(enemyArray!=null){


                foreach (GameObject thisEnemy in enemyArray){
                    thisEnemy.GetComponent<EnemyMovement>().Slowed(2f,0.5f);
                    thisEnemy.GetComponent<EnemyStats>().TakeDamage(20+GameObject.Find("Player").GetComponent<PlayerStats>().GetStrength());
                    
                    thisEnemy.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
                    
                    
                }
                
                
                
        }
    
    sfxScript.PlaySound("converge_splash");
    yield return new WaitForSeconds(0.5f);
    
        Destroy(this.gameObject);
    

    
    

    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            Debug.Log("converge collidwed withj enemy "+other);
            enemyArray.Add(other.gameObject);

        }
        
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Enemy"){
           
            Debug.Log("converge left enemy hitbox "+other);
            
            enemyArray.Remove(other.gameObject);
        
        }
    }


}
