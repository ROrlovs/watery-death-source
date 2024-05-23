using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public enum SpawnerType{Straight, Spin, Player}
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed =1f;


    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate =1f;

    private GameObject spawnedBullet;
    private float timer = 0f;

    private Rigidbody2D rb;
    private GameObject player;

    EnemySoundScript sfxScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        
        sfxScript = gameObject.transform.parent.gameObject.GetComponent<EnemyStats>().GetSFXScript();
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;

        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z+1f);

        float randomAngle=Random.Range(-15,15);

        if (spawnerType == SpawnerType.Player){
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f,0f,angle+randomAngle);
            direction.Normalize();
        }




        if (timer >= firingRate){
            Fire();
            
        }
    }

    private void Fire(){
        if(bullet){
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
            timer = 0;
            sfxScript.PlaySound("enemy_shoot");
        }
    }

    public void SetSpawnerType(SpawnerType type){
        spawnerType = type;
    }
}

/*
Ideas:
    - Predicted player position

*/