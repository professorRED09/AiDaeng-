using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Subject, IObserver
{
    //[Header("Scene & UI")]
    [SerializeField] GameObject resultPanel;
    [SerializeField] TMP_Text resultText;
    //[SerializeField] GameObject pausePanel;
    //[SerializeField] GameObject settingPanel;
    //[SerializeField] GameObject tutorialPanel;

    ////[SerializeField] GameObject

    [Header("All Manager")]
    public NPC_Manager2 npcManager;
    public ScoreManager scoreManager;
    public WeatherManager weatherManager;

    [Header("Level timer")]
    [SerializeField] Timer timer;
    //[SerializeField] private TMP_Text levelTimerText;
    //[SerializeField] private float timeToDisplay = 60.0f;
    //private bool _isRunning;

    [SerializeField] GameObject StartNotif;
    [SerializeField] GameObject ClosedNotif;


    [SerializeField] float startTimer;    

    public bool isGameover = false;    

    private void Start()
    {
        isGameover = false;

        npcManager.enabled = false;
        scoreManager.enabled = false;
        weatherManager.enabled = false;
        StartCoroutine(countdownToStart()); // start coroutine
    }

    private void OnEnable()
    {        
        EventManager.TimerStop += EventManagerOnTimeStop;
    }

    private void OnDisable()
    {
        EventManager.TimerStop -= EventManagerOnTimeStop;
    }

    void EventManagerOnTimeStop()
    {
        ClosedStore();  //show "store closed" pop up        
    }

    void ClosedStore()
    {
        ClosedNotif.GetComponent<Animator>().SetTrigger("isTimeout");
    }

    IEnumerator countdownToStart()
    {
        StartNotif.GetComponent<Animator>().SetTrigger("Enter");    // play animation start pop up
        yield return new WaitForSeconds(3); // this make the game kinda jiggle, 
        StartNotif.GetComponent<Animator>().SetTrigger("Leave");
        timer.StartLevelTimer();
        npcManager.enabled = true;
        scoreManager.enabled = true;
        weatherManager.enabled = true;      

    }

    //public void StartGame()
    //{
    //    SceneManager.LoadScene("Cutscene1");
    //}

    //public void ShowSetting()
    //{
    //    settingPanel.SetActive(true);
    //}

    //public void ShowTutorial()
    //{
    //    tutorialPanel.SetActive(true);
    //}

    //public void BackFromSetting()
    //{
    //    settingPanel.SetActive(false);
    //}

    //public void BackFromTutorial()
    //{
    //    tutorialPanel.SetActive(false);
    //}

    

    public void ShowResult()
    {
        if (isGameover == true)
            return;
        resultPanel.SetActive(true);
        if (ScoreManager.instance.DoesPlayerWin() == true)
        {
            resultText.text = "ผ่านด่าน!";
        }
        else
        {
            resultText.text = "ลองเล่นใหม่!";
        }
        print("VICTOR");
        isGameover = true;
    }

    //public void PauseGame()
    //{
    //    pausePanel.SetActive(true);
    //    Time.timeScale = 0;
    //}

    //public void ResumeGame()
    //{
    //    pausePanel.SetActive(false);
    //    Time.timeScale = 1;
    //}

    //public void ShowLevelSelect()
    //{
    //    SceneManager.LoadScene("LevelSelect");
    //}

    //public void PlayGame()
    //{
    //    SceneManager.LoadScene("Gameplay1");
    //}

    //public void BackFromLevelSelect()
    //{
    //    SceneManager.LoadScene("Title");
    //}

    public void OnNotify(WeatherType weather)
    {

    }
    public void OnNotify(Action action)
    {

    }


}
