using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DontDestroy : MonoBehaviour    
{
    public AudioMixer audioMixer;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //print(PlayerPrefs.GetFloat("masterVolume"));
        //print(PlayerPrefs.GetFloat("musicVolume"));
        //print(PlayerPrefs.GetFloat("soundFXVolume"));        
            
        audioMixer.SetFloat("masterVolume", Mathf.Log10(PlayerPrefs.GetFloat("masterVolume")) * 20f);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20f);
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("soundFXVolume")) * 20f);
        
    }
}
