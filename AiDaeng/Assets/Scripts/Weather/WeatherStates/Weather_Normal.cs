using UnityEngine;

public class Weather_Normal : Weather_BaseState
{
    private float _timer = 5f;
    private float _currentTimer;

    public override void EnterState(WeatherManager weather)
    {
        weather.anim.SetTrigger("isNormal");

        _currentTimer = _timer;

        // return everything to normal
        //weather.anim.SetTrigger("isNormal");
        //weather.currentTimer = weather.timer;
        //Debug.Log("NORMAL STATE ENTRY");
    }

    public override void UpdateState(WeatherManager weather)
    {
        // countdown timer
        _currentTimer -= Time.deltaTime; 
        if (_currentTimer <= 0)
        {
            weather.anim.ResetTrigger("isNormal");

            weather.SwitchState(weather.rainyState);
        }

        //weather.currentTimer -= Time.deltaTime; // countdown timer
        //if (weather.currentTimer <= 0)
        //{
        //    weather.anim.ResetTrigger("isNormal");
        //    weather.SwitchState(weather.sunnyState);
        //}

        // if hit jackpot, change weather again
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
