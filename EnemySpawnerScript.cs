using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
     [SerializeField] GameObject enemyObject;

     

     [SerializeField] private int maxNumberOfEnemies;
     
     private int tempWaveEHolder;

     WaveManagerScript wave;

     private void Start() {
          
          wave = GameObject.Find("WaveManager").GetComponent<WaveManagerScript>();
          InvokeRepeating("SpawnEnemy",1,1);
          
     }
   private void SpawnEnemy(){



          if(wave.CanSpawnEnemy()){
               GameObject newEnemy = Instantiate(enemyObject,transform);
               wave.AddEnemy(newEnemy);
          }
          
          

          
     

     else{
          Debug.Log("Enemy not allowed to spawn!");
     }
     
   }



}
