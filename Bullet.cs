using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;

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
        return new Vector2(x+spawnPoint.x, y+spawnPoint.y);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            Debug.Log("damage taken");
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damageToDeal+(GameObject.Find("WaveManager").GetComponent<WaveManagerScript>().GetCurrentWave()));
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Wall"){
            Destroy(this.gameObject);
        }
    }

}
