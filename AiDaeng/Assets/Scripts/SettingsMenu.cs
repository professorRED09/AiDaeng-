using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsPanel;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetSoundFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void OnPressBackButton()
    {
        settingsPanel.SetActive(false);
    }
}
