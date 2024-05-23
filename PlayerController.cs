using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour {
 
    public float speed;
    private Rigidbody2D rb2d;
    private TimeManagerScript globalTimeManager;

    public GameObject bullet;
    private GameObject spawnedBullet;

    [SerializeField] private float firingRate =1f;

    public float bulletLife = 1f;
    private float timer = 0f;

    private bool ability1_canfire;
    private bool ability2_canfire;
    private bool ability3_canfire;

    public bool currentlyShooting;

    private Animator animator;

    [SerializeField] private GameObject ability1;
    [SerializeField] private GameObject ability2;

    [SerializeField] private GameObject playerSpriteObj;

    SoundEffectScript sfxScript;
 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        globalTimeManager = GameObject.Find("TimeManager").GetComponent<TimeManagerScript>();
        playerSpriteObj = GameObject.Find("PlayerSprite");
        animator = GameObject.Find("PlayerSprite").GetComponent<Animator>();
        ability1_canfire = true;
        ability2_canfire = true;
        ability3_canfire = true;
        sfxScript = GameObject.FindGameObjectWithTag("PlayerAudioSource").GetComponent<SoundEffectScript>();
    }
 
    void Update()
    {

        timer++;

        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        playerSpriteObj.GetComponent<PlayerSpriteFollowGameObject>().ChangePosition(transform);




        if (Input.GetKeyDown(KeyCode.Minus)){
            GetComponent<PlayerStats>().IncrementLevel();
        }

        if (Input.GetMouseButtonDown(0)){
            if (timer >= firingRate){
            StartCoroutine(AttemptToShoot()); 
        }
        }

        if (Input.GetKeyDown(KeyCode.Z)){
            //Debug.Log("pressed ability 1 ");
            if(ability1_canfire){
                GameObject abilityObj = Instantiate(ability1,this.transform);
                StartCoroutine(CooldownAbility1(abilityObj.GetComponent<Ability_FrozenBreath>().cooldown));
            }
            
        }

        if (Input.GetKeyDown(KeyCode.X)){
            //Debug.Log("pressed ability 2");
            if(ability2_canfire){
                GameObject abilityObj = Instantiate(ability2,this.transform);
                StartCoroutine(CooldownAbility2(abilityObj.GetComponent<Ability_Converge>().cooldown));
            }
        }
 
        rb2d.velocity = new Vector2 (moveHorizontal*speed, moveVertical*speed);
        Vector3 lookDirection = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float lookAngle = Mathf.Atan2(-lookDirection.y, -lookDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f,0f,lookAngle-90);

 
        // Try out this delta time method??
        //rb2d.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

        if(currentlyShooting){
            animator.Play("Player_Cast");
        }
        else{
            animator.Play("Player_Idle");
        }


    }

    void ShootPlayerBullet(){


        Vector3 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(direction);
        direction.Normalize();
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        

        spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        spawnedBullet.GetComponent<PlayerBullet>().speed = speed;
        spawnedBullet.GetComponent<PlayerBullet>().bulletLife = bulletLife;
        spawnedBullet.transform.eulerAngles = new Vector3(0f,0f,angle);
        timer = 0;
        

        
    }

    IEnumerator CooldownAbility1(float cooldown){
        ability1_canfire=false;
        yield return new WaitForSeconds(cooldown);
        ability1_canfire=true;
    }

    IEnumerator CooldownAbility2(float cooldown){
        ability2_canfire=false;
        yield return new WaitForSeconds(cooldown);
        ability2_canfire=true;
    }

    IEnumerator CooldownAbility3(float cooldown){

        yield return new WaitForSeconds(cooldown);
    }

    IEnumerator AttemptToShoot(){
        if(!currentlyShooting){
            currentlyShooting = true;
            sfxScript.PlaySound("generic_windup");
            yield return new WaitForSeconds(0.5f);
            ShootPlayerBullet();
            sfxScript.PlaySound("generic_shoot");
            currentlyShooting=false;
        }
        else{
            //Debug.Log("cant fire! still shooting...");
        }
        
        
    }
 
    
}
