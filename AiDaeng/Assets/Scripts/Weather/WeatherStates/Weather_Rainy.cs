using UnityEngine;

public class Weather_Rainy : Weather_BaseState
{
    private float _timer = 200f;
    private float _currentTimer;

    public override void EnterState(WeatherManager weather)
    {
        //play rainy atmosphere
        weather.anim.SetTrigger("isRainy");
        weather.rainFX.Play();

        _currentTimer = _timer;
        //weather.currentTimer = weather.timer;
        //Debug.Log("RAINY STATE ENTRY");
    }

    public override void UpdateState(WeatherManager weather)
    {
        _currentTimer -= Time.deltaTime; // countdown timer
        if (_currentTimer <= 0)
        {
            weather.anim.ResetTrigger("isRainy");
            weather.rainFX.Stop();

            weather.SwitchState(weather.normalState);
        }

        //weather.currentTimer -= Time.deltaTime; // countdown timer
        //if (weather.currentTimer <= 0)
        //{
        //    weather.anim.ResetTrigger("isRainy");
        //    weather.rainFX.Stop();
        //    weather.SwitchState(weather.normalState);
        //}


        // if hit jackpot, change weather again
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
