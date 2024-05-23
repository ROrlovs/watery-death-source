using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritManagerScript : MonoBehaviour
{


public bool RollCrit(float crit_chance){
    bool crit;
    int rand = Random.Range(0,100);

    if(rand <= crit_chance){
        crit = true;
        Debug.Log("CRIT ROLLED");
    } 
    else crit=false;

    return crit;
}


}
