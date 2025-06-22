using UnityEngine;

public class Weather_Sunny : Weather_BaseState
{
    public override void EnterState(WeatherManager weather)
    {
        weather.anim.SetTrigger("isSunny");
        weather.currentTimer = weather.timer;
        //Debug.Log("Sunny STATE ENTRY");
    }

    public override void UpdateState(WeatherManager weather)
    {
        weather.currentTimer -= Time.deltaTime; // countdown timer
        if (weather.currentTimer <= 0)
        {
            weather.anim.ResetTrigger("isSunny");            
            weather.SwitchState(weather.normalState);
        }
            

        // if hit jackpot, change weather again
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
