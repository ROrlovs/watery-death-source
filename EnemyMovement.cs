using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float speed;
    public bool slowed;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GetComponent<EnemyStats>().GetSpeed();
        InvokeRepeating("FollowPlayerDirection",0.5f,0.5f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slowed(float factor, float time){
        StartCoroutine(SlowedEffect(factor,time));
    }

    private IEnumerator SlowedEffect(float factor, float time){
        
        if(!slowed){
            slowed=true;
            Debug.Log(this.gameObject + " enemy is slowed for "+factor+" and time "+time);
            speed = speed/factor;
            GetComponent<SpriteRenderer>().color=Color.cyan;
            yield return new WaitForSeconds(time);
            Debug.Log(this.gameObject + " is no longer slwoed ");
            GetComponent<SpriteRenderer>().color=Color.white;
            speed=GetComponent<EnemyStats>().GetOriginalSpeed();
            slowed=false;
        }
        else{
            //Debug.Log("Enemy Already Slowed");
        }
        
    }

    private void RandomDirections(){
        //speed = GetComponent<EnemyStats>().GetSpeed();
        rb.velocity = new Vector2(Random.Range(-1,2)*speed,Random.Range(-1,2)*speed);
    }

    private void FollowPlayerDirection(){
        if(target){
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity=new Vector2(direction.x+Random.Range(-1,2),direction.y+Random.Range(-1,2))*speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("enemy collided with something "+other);
        
    }


}
