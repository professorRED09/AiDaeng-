using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip newMusic;

    private void Awake()
    {
        var sound = GameObject.FindWithTag("Music");
        if(sound != null)
        {
            AudioSource audioSource = sound.GetComponent<AudioSource>();
            audioSource.clip = newMusic;
            audioSource.Play();
        }
        
    }
}
