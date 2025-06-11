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

    public float timer;
    public float currentTimer;

    //public bool Rainy;
    //public bool Sunny;

    //FSM Stuff
    public Weather_BaseState currentState;
    public Weather_Normal normalState = new Weather_Normal();
    public Weather_Rainy rainyState = new Weather_Rainy();
    public Weather_Sunny sunnyState = new Weather_Sunny();

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

    //public void OnNotify(WeatherType weather)
    //{

    //}
    //public void OnNotify(Action action)
    //{
    //    switch (action)
    //    {
    //        case (Action.StartTimerStop):

    //            return;
    //        default:
    //            return;
    //    }
    //}

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
