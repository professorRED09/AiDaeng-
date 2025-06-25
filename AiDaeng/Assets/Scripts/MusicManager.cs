using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    GameObject sound;
    AudioSource audioSource;

    public AudioClip sceneMusic;
    
    [Header("Music Ref")]
    public AudioClip levelBeginMusic;
    public AudioClip gameplayMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;

    private void Awake()
    {
        sound = GameObject.FindWithTag("Music");
        if(sound != null)
        {
            audioSource = sound.GetComponent<AudioSource>();
            if(sceneMusic != null)
            {
                audioSource.clip = sceneMusic;
                audioSource.Play();
            }            
        }        
    }

    public IEnumerator PlayBeginSound()
    {
        audioSource.clip = levelBeginMusic;
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        audioSource.clip = gameplayMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void PlayResultMusic(float bowlCount)
    {
        if(bowlCount >= 1)
        {
            audioSource.clip = winMusic;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = loseMusic;
            audioSource.Play();
        }

        audioSource.loop = false;
    }
}
