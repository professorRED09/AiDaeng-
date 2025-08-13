using UnityEngine;
using DG.Tweening;

public class Weather_Sunny : Weather_BaseState
{
    private float _timer = 40f;
    private float _currentTimer;

    public override void EnterState(WeatherManager weather)
    {
        Debug.Log("ENTER SUNNY WEATHER");
        ////play sunny atmosphere
        ////weather.anim.SetTrigger("isSunny");

        weather.sunnyFilter.GetComponent<SpriteRenderer>().DOFade(0.24f, 2f);
        _currentTimer = _timer;

        //weather.audioSource.clip = weather.sunnyWeatherSFX;
        //weather.audioSource.Play();
    }

    public override void UpdateState(WeatherManager weather)
    {
        _currentTimer -= Time.deltaTime; // countdown timer
        if (_currentTimer <= 0)
        {
            //weather.anim.ResetTrigger("isSunny");
            weather.sunnyFilter.GetComponent<SpriteRenderer>().DOFade(0f, 2f);

            weather.ChangeToNextWeather();
            //weather.SwitchState(weather.normalState);
        }
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
