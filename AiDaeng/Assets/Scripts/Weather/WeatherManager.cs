using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : Subject, IObserver
{
    public GameObject weatherFilter;
    public Animator anim;
    public ParticleSystem rainFX;
    public float timer;
    public float currentTimer;

    public bool Rainy;
    public bool Sunny;

    //FSM Stuff
    Weather_BaseState currentState;
    public Weather_Normal normalState = new Weather_Normal();
    public Weather_Rainy rainyState = new Weather_Rainy();


    void Awake()
    {
        
    }

    void Start()
    {
        weatherFilter.SetActive(true);

        currentState = normalState;
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

    public void OnNotify(WeatherType weather)
    {

    }
    public void OnNotify(Action action)
    {
        switch (action)
        {
            case (Action.StartTimerStop):

                return;
            default:
                return;
        }
    }

    //public void OnNotify(WeatherType weather)
    //{
    //    switch (weather)
    //    {
    //        case (WeatherType.Rainy):
    //            anim.SetTrigger("isRainy");
    //            //SwitchState(rainyState);
    //            return;

    //        case (WeatherType.Normal):
    //            anim.SetTrigger("isNormal");
    //            //SwitchState(normalState);
    //            return;

    //        default:
    //            return;
    //    }
    //}

    //void OnEnable()
    //{
    //    weatherController.AddObserver(this);
    //}

    //void OnDisable()
    //{
    //    weatherController.RemoveObserver(this);
    //}

}
