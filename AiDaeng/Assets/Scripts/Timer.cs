using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Image _timerImageFill;
    [SerializeField] private float timeToDisplay = 60.0f;
    private float currentTimer;
    private bool _isRunning;
    TimeSpan timeSpan;

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;

        currentTimer = timeToDisplay;
        timeSpan = TimeSpan.FromSeconds(currentTimer);
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
        if (currentTimer < 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }

        currentTimer -= Time.deltaTime;
        float targetFillAmount = Math.Abs(timeToDisplay - currentTimer) / timeToDisplay;
        _timerImageFill.fillAmount = targetFillAmount;

        timeSpan = TimeSpan.FromSeconds(currentTimer);    //  represent timeToDisplay in seconds with timeSpan variable
        _timerText.text = timeSpan.ToString(@"mm\:ss");         //  display time in minute:second form        

    }
}
