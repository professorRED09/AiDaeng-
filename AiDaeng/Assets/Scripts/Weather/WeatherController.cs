//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WeatherController : Subject
//{
//    private void Start()
//    {
//        StartCoroutine(ChangeWeatherRoutine(5f));
//    }

//    private IEnumerator ChangeWeatherRoutine(float waitingTime)
//    {        
//        yield return new WaitForSeconds(waitingTime); // Change weather every 5 seconds
//        NotifyObserver(WeatherType.Rainy);
        
//    }
//}
