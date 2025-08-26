using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Image _timerImageFill;

    [SerializeField] private RectTransform _timerKem;
    private float _startAngle;
    private float _endAngle;

    [SerializeField] private float timeToDisplay;
    private float currentTimer;
    private bool _isRunning;
    TimeSpan timeSpan;

    public bool unlimitedTimeCheck;

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
        _startAngle = _timerKem.eulerAngles.z;
        _endAngle = _startAngle + 360f;
    }

    private void Update()
    {
        //countdown game timer        
        if (!_isRunning || unlimitedTimeCheck) return;

        //When the time is out, invoke OnTimeStop() function
        if (currentTimer < 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }

        currentTimer -= Time.deltaTime;
        float targetFillAmount = Math.Abs(timeToDisplay - currentTimer) / timeToDisplay;
        _timerImageFill.fillAmount = targetFillAmount;


        float t = Mathf.Clamp01(currentTimer / timeToDisplay);
        float currentAngle = Mathf.Lerp(_startAngle, _endAngle, t);
        _timerKem.localEulerAngles = new Vector3(0, 0, currentAngle);
        //_timerKem.transform.Rotate(0, 0, )

        timeSpan = TimeSpan.FromSeconds(currentTimer);    //  represent timeToDisplay in seconds with timeSpan variable
        _timerText.text = timeSpan.ToString(@"mm\:ss");         //  display time in minute:second form        

    }
}
