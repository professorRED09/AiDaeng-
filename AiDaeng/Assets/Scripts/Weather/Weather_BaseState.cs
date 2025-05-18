using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weather_BaseState 
{
    public abstract void EnterState(WeatherManager weather);
    public abstract void UpdateState(WeatherManager weather);
    public abstract void ExitState(WeatherManager weather);
}
