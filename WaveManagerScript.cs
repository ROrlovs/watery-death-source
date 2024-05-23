using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class WaveManagerScript : MonoBehaviour
{

    private int currentWave;
    private int enemiesToSpawn;
    private bool enemiesAllowedToSpawn;
    private bool waveInProgress;
    public int waveTime;
    private List<GameObject> enemyList = new List<GameObject>();
    private int enemySpawnedCount;
    private int enemiesKilled;

    void Start(){
        InvokeRepeating("WaveTimer",5,waveTime);
    }

    void WaveIncrement(){
        currentWave++;
        enemiesToSpawn+=6;
        enemySpawnedCount=0;
        GameObject.FindGameObjectWithTag("CurrentWaveText").GetComponent<TextMeshProUGUI>().text = "CURRENT WAVE: "+currentWave;
        enemiesAllowedToSpawn=true;
        waveInProgress = true;
    }

    public void IncrementEnemiesKilled(){
        enemiesKilled++;
        GameObject.FindGameObjectWithTag("EnemiesKilledText").GetComponent<TextMeshProUGUI>().text = "Enemies killed: "+enemiesKilled;
        if(enemiesKilled % 5 == 0){
            GameObject.Find("Player").GetComponent<PlayerStats>().IncrementLevel();
            Debug.Log("-----LEVEL UP -----");
        }
    }

    public int GetEnemiesKilled(){
        return enemiesKilled;
    }

    public void AddEnemy(GameObject enemyToAdd){
        enemySpawnedCount++;
        enemyList.Add(enemyToAdd);
    }

    public void RemoveEnemy(GameObject enemyToRemove){
        enemyList.Remove(enemyToRemove);
    }

    public int EnemyCount(){
        return enemyList.Count;
    }

    public bool CanSpawnEnemy(){
        bool canspawn;
        if(!(enemySpawnedCount>enemiesToSpawn)){
            Debug.Log("enemy is allowed to spawn. enemy spawn count is : "+enemySpawnedCount+", maximum amount is "+enemiesToSpawn);
            canspawn=true;
        }

        else{
            canspawn = false;
        }
        return canspawn;
    }

    public int GetCurrentWave(){
        return currentWave;
    }

    public int GetEnemiesToSpawn(){
        return enemiesToSpawn;
    }

    public bool GetEnemiesCanSpawn(){
        return enemiesAllowedToSpawn;
    }

    private IEnumerator WaveEnd(){
        enemiesAllowedToSpawn = false;
        waveInProgress = false;
        Debug.Log("Wave ended...");
        yield return new WaitForSeconds(5);
        Debug.Log("Wave Starting");
    }


    private void WaveTimer(){
        if(!waveInProgress){
            WaveIncrement();
        }

        else{
            StartCoroutine(WaveEnd());
        }
        
    }

}
