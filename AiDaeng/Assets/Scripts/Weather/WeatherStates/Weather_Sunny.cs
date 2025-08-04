using UnityEngine;

public class Weather_Sunny : Weather_BaseState
{
    private float _timer = 20f;
    private float _currentTimer;

    public override void EnterState(WeatherManager weather)
    {
        //play sunny atmosphere
        weather.anim.SetTrigger("isSunny");

        _currentTimer = _timer;

        weather.audioSource.clip = weather.sunnyWeatherSFX;
        weather.audioSource.Play();
    }

    public override void UpdateState(WeatherManager weather)
    {
        _currentTimer -= Time.deltaTime; // countdown timer
        if (_currentTimer <= 0)
        {
            weather.anim.ResetTrigger("isSunny");
            weather.SwitchState(weather.normalState);
        }
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
