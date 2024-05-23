using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public AudioClip generic_shoot;
    public AudioClip windup;
    public AudioClip frozenbreath;
    public AudioClip converge_windup;
    public AudioClip converge_splash;
    public AudioClip pickup;
    public AudioClip playerdeath;
    public AudioClip playerhit;

    AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound){
        audioSrc.pitch = Random.Range(0.7f,1.5f);
        if (sound=="generic_shoot"){
            audioSrc.PlayOneShot(generic_shoot);
        }

        if(sound=="generic_windup"){
            audioSrc.PlayOneShot(windup);
        }

        if(sound=="frozenbreath"){
            audioSrc.PlayOneShot(frozenbreath);
        }

        if(sound=="converge_windup"){
            audioSrc.PlayOneShot(converge_windup);
        }

        if(sound=="converge_splash"){
            audioSrc.PlayOneShot(converge_splash);
        }

        if(sound=="pickup"){
            audioSrc.PlayOneShot(pickup);
        }

        if(sound=="player_death"){
            audioSrc.PlayOneShot(playerdeath);
        }

        if(sound=="player_hit"){
            audioSrc.PlayOneShot(playerhit);
        }
    }
}
