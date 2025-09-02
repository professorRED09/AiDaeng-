using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : Subject
{
    public GameObject weatherFilter;
    public Animator anim;

    [Header("Sunny")]
    public GameObject sunnyFilter;

    [Header("Rainy")]
    public GameObject rainFilter;
    public ParticleSystem rainDropParticle;
    public GameObject rainAnimation;

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

    public int[] weatherOrderArray;
    public float[] weatherTimerArray;
    //[SerializeField] private int _currentIndex = -1;
    public int currentIndex = -1;
    [SerializeField] private int _currentWeatherOrder;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //weatherFilter.SetActive(true);

        //currentState = normalState;
        //currentState.EnterState(this);

        ChangeToNextWeather();
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

    public void ChangeToNextWeather()
    {
        if(currentIndex < weatherOrderArray.Length-1)
        {
            currentIndex++;
            _currentWeatherOrder = weatherOrderArray[currentIndex];
        }
        else
        {
            return;
        }

        if(_currentWeatherOrder == 1)
        {
            SwitchState(normalState);
        }
        else if(_currentWeatherOrder == 2)
        {
            SwitchState(sunnyState);
        }
        else
        {
            SwitchState(rainyState);
        }
    }

}
