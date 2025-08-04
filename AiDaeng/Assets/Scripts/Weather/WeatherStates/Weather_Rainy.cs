using UnityEngine;

public class Weather_Rainy : Weather_BaseState
{
    private float _timer = 30f;
    private float _currentTimer;

    public override void EnterState(WeatherManager weather)
    {
        //play rainy atmosphere
        weather.anim.SetTrigger("isRainy");
        weather.rainFX.Play();

        _currentTimer = _timer;

        weather.audioSource.clip = weather.rainyWeatherSFX;
        weather.audioSource.Play();
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
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
