using UnityEngine;

public class Weather_Rainy : Weather_BaseState
{
    public override void EnterState(WeatherManager weather)
    {
        weather.anim.SetTrigger("isRainy");
        weather.rainFX.Play();
        weather.currentTimer = weather.timer;
        Debug.Log("RAINY STATE ENTRY");
    }

    public override void UpdateState(WeatherManager weather)
    {
        weather.currentTimer -= Time.deltaTime; // countdown timer
        if (weather.currentTimer <= 0)
        {
            weather.anim.ResetTrigger("isRainy");
            weather.rainFX.Stop();
            weather.SwitchState(weather.normalState);
        }
            

        // if hit jackpot, change weather again
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
