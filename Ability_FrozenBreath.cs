using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Ability_FrozenBreath : MonoBehaviour
{

    private bool colliding;
    private int timer;

    private Animator animator;

    private GameObject enemy;

    public float cooldown;

    private List<GameObject> enemyArray = new List<GameObject>();

    [SerializeField] float tick_frequency = 0.5f;

    [SerializeField] float length_of_frozen_breath = 5f;

    public int ability_damage;

    private int original_damage_to_do;

    SoundEffectScript sfxScript;

    void Start(){
        timer=0;
        animator = GameObject.Find("PlayerSprite").GetComponent<Animator>();
        animator.Play("Player_Cast");
        InvokeRepeating("FrozenBreathFire",0.5f,tick_frequency);
        original_damage_to_do = ability_damage;
        sfxScript = GameObject.FindGameObjectWithTag("PlayerAudioSource").GetComponent<SoundEffectScript>();
        sfxScript.PlaySound("frozenbreath");
    }




    private void FrozenBreathFire(){
        
        
        //Debug.Log("frozen breath coroutune");

        
        
        if(timer<=length_of_frozen_breath){
            

            if(enemyArray!=null){


                    foreach (GameObject thisEnemy in enemyArray){
                        //float enemySpeed = thisEnemy.GetComponent<EnemyStats>().GetSpeed();
                        //thisEnemy.GetComponent<EnemyStats>().ChangeSpeed(enemySpeed/2);
                        ability_damage=original_damage_to_do;
                        thisEnemy.GetComponent<EnemyMovement>().Slowed(5f,2f);
                        bool crit = GameObject.Find("CritManager").GetComponent<CritManagerScript>().RollCrit(20+GameObject.Find("Player").GetComponent<PlayerStats>().GetStrength()/2);
                        if (crit){
                            ability_damage*=3;
                            thisEnemy.gameObject.GetComponentInChildren<CritTextScript>().Crit();
                        } 
                        thisEnemy.GetComponent<EnemyStats>().TakeDamage(ability_damage+GameObject.Find("Player").GetComponent<PlayerStats>().GetStrength());
                    }
                    
                    
                    
            }

            }

        else{

            //Debug.Log("Destroying friozen breath");

            animator.Play("Player_Idle");
            Destroy(this.gameObject);

        }

        

        timer++;
       
        
        

        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            Debug.Log("frozen breath collidwed withj enemy "+other);
            enemyArray.Add(other.gameObject);

        }
        
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Enemy"){
           
            Debug.Log("frozen breath left enemy hitbox "+other);
            
            enemyArray.Remove(other.gameObject);
        
        }
    }


}
