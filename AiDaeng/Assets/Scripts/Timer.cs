using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float timeToDisplay = 60.0f;
    private bool _isRunning;
    TimeSpan timeSpan;

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;

        timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        _timerText.text = timeSpan.ToString(@"mm\:ss");
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
    }

    private void EventManagerOnTimerStart() => _isRunning = true;
    private void EventManagerOnTimerStop() => _isRunning = false;

    public void StartLevelTimer()
    {
        EventManager.OnTimerStart();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        //countdown game timer        
        if (!_isRunning) return;

        //When the time is out, invoke OnTimeStop() function
        if (timeToDisplay < 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }

        timeToDisplay -= Time.deltaTime;

        timeSpan = TimeSpan.FromSeconds(timeToDisplay);    //  represent timeToDisplay in seconds with timeSpan variable
        _timerText.text = timeSpan.ToString(@"mm\:ss");         //  display time in minute:second form        

    }
}
