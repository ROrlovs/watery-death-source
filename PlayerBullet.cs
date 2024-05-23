using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed;

    private Vector2 spawnPoint;
    private float timer = 0.0f;

    private float damageToDeal = 10;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer+= Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer){
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x*speed+spawnPoint.x, y*speed+spawnPoint.y);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            Debug.Log("damage taken");
            bool crit = GameObject.Find("CritManager").GetComponent<CritManagerScript>().RollCrit(20+GameObject.Find("Player").GetComponent<PlayerStats>().GetStrength()/2);
            if (crit){
                damageToDeal*=3;
                other.gameObject.GetComponentInChildren<CritTextScript>().Crit();
            } 
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(damageToDeal+GameObject.Find("Player").GetComponent<PlayerStats>().GetStrength());
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Wall"){
            Destroy(this.gameObject);
        }

        
    }



}
