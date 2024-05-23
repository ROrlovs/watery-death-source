using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundScript : MonoBehaviour
{

    public AudioClip skeleton;
    public AudioClip skeletondeath;
    public AudioClip enemyhit;
    public AudioClip enemyshoot;
    AudioSource audioSrc;

    void Start()
        {
            audioSrc = GetComponent<AudioSource>();
        }

    public void PlaySound(string sound){
        audioSrc.pitch = Random.Range(0.7f,1.5f);


        if(sound=="skeleton_death"){
            audioSrc.PlayOneShot(skeletondeath);
        }

        if(sound=="enemy_hit"){
            audioSrc.PlayOneShot(enemyhit);
        }

        if(sound=="enemy_shoot"){
            audioSrc.PlayOneShot(enemyshoot);
        }

}


}
