using UnityEngine;
using DG.Tweening;

public class Weather_Rainy : Weather_BaseState
{
    private float _timer = 40f;
    private float _currentTimer;

    public override void EnterState(WeatherManager weather)
    {
        ////play rainy atmosphere
        ////weather.anim.SetTrigger("isRainy");
        //weather.rainFilter.GetComponent<SpriteRenderer>().enabled = true;
        weather.rainFilter.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        weather.rainAnimation.GetComponent<SpriteRenderer>().DOFade(0.69f, 2f);
        weather.rainAnimation.GetComponent<Animator>().enabled = true;
        weather.rainDropParticle.Play();

        _currentTimer = _timer;

        //weather.audioSource.clip = weather.rainyWeatherSFX;
        //weather.audioSource.Play();
    }

    public override void UpdateState(WeatherManager weather)
    {
        // countdown timer
        _currentTimer -= Time.deltaTime; 
        
        if (_currentTimer <= 0)
        {
            ////weather.anim.ResetTrigger("isRainy");
            //weather.rainFilter.GetComponent<SpriteRenderer>().enabled = false;
            weather.rainFilter.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            weather.rainAnimation.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            weather.rainAnimation.GetComponent<Animator>().enabled = false;
            weather.rainDropParticle.Stop();


            weather.ChangeToNextWeather();
            //weather.SwitchState(weather.normalState);
        }
    }

    public override void ExitState(WeatherManager weather)
    {
        
    }
}
