using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropsManagerScript : MonoBehaviour
{
    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;

    public void DropItem(int rarity, GameObject enemy){
        int rand = Random.Range(0,rarity);
        if(rand>=0){
            
        }

        if(rand>=20 && rand<40){
            Instantiate(drop1,new Vector3(enemy.transform.position.x,enemy.transform.position.y,enemy.transform.position.z),Quaternion.identity);
        }

        if(rand>=40 && rand<60){
            Instantiate(drop2,enemy.transform);
        }

        if(rand>=60){
            Instantiate(drop3,enemy.transform);
        }
    }
}
