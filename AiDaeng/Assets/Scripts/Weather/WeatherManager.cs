using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : Subject
{
    public GameObject weatherFilter;
    public Animator anim;
    [Header("Sunny")]

    [Header("Rainy")]
    public ParticleSystem rainFX;

    //private float weatherTimer;
    //private float currentWeatherTimer;    
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip normalWeatherSFX;
    public AudioClip sunnyWeatherSFX;
    public AudioClip rainyWeatherSFX;

    //FSM Stuff
    public Weather_BaseState currentState;
    public Weather_Normal normalState = new Weather_Normal();
    public Weather_Rainy rainyState = new Weather_Rainy();
    public Weather_Sunny sunnyState = new Weather_Sunny();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        weatherFilter.SetActive(true);

        currentState = normalState;
        //currentState = rainyState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(Weather_BaseState state)
    {
        currentState = state;

        if (currentState == normalState)
        {
            NotifyObserver(WeatherType.Normal);
        }
        else if (currentState == rainyState)
        {
            NotifyObserver(WeatherType.Rainy);
        }
        else
        {
            NotifyObserver(WeatherType.Sunny);
        }

        state.EnterState(this);
    }

}
