using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour
{
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    public void BulletTimeActivate(){

        if(active){
            active=false;
            Time.timeScale = 1f;
            //Debug.Log(Time.fixedDeltaTime);
            //breaks game
            //Time.fixedDeltaTime = 1;
        }

        else{
            active=true;
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        
    }


}
