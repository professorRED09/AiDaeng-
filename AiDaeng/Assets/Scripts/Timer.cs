using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text _timerText;
    [SerializeField] private float timeToDisplay = 60.0f;
    private bool _isRunning; 

    private void Awake() => _timerText = GetComponent<TMP_Text>();

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
    }

    private void EventManagerOnTimerStart() => _isRunning = true;
    private void EventManagerOnTimerStop() => _isRunning = false;

    private void Start()
    {
        EventManager.OnTimerStart();
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

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);    //  represent timeToDisplay in seconds with timeSpan variable
        _timerText.text = timeSpan.ToString(@"mm\:ss");         //  display time in minute:second form        

    }
}
