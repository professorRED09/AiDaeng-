using UnityEngine;

public class Weather_Normal : Weather_BaseState
{
    public override void EnterState(WeatherManager weather)
    {
        // return everything to normal
        weather.anim.SetTrigger("isNormal");
        weather.currentTimer = weather.timer;
        //Debug.Log("NORMAL STATE ENTRY");
    }

    public override void UpdateState(WeatherManager weather)
    {        
        weather.currentTimer -= Time.deltaTime; // countdown timer
        if (weather.currentTimer <= 0)
        {
            weather.anim.ResetTrigger("isNormal");
            weather.SwitchState(weather.rainyState);
        }
            
        // if hit jackpot, change weather again
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
