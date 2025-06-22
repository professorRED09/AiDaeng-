using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Subject, IObserver
{
    [Header("UI")]
    [SerializeField] GameObject resultPanel;
    [SerializeField] TMP_Text resultText;
    [SerializeField] GameObject StartNotif;
    [SerializeField] GameObject ClosedNotif;

    // Managers
    private NPC_Manager2 npcManager;
    private ScoreManager scoreManager;
    private WeatherManager weatherManager;

    [Header("Level timer")]
    [SerializeField] Timer timer;
    [SerializeField] float startTimer;    

    public bool isGameover = false;

    private PlayerController1 player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController1>();
        npcManager = FindObjectOfType<NPC_Manager2>();
        scoreManager = FindObjectOfType<ScoreManager>();
        weatherManager = FindObjectOfType<WeatherManager>();
    }

    private void Start()
    {        
        StartCoroutine(countdownToStart());     // start coroutine
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
        yield return new WaitForSeconds(3);                         // this make the game kinda jiggle, find a way to fix it later 
        StartNotif.GetComponent<Animator>().SetTrigger("Leave");
        timer.StartLevelTimer();

        EnableManagers();
        player.canControl = true;
    }    

    private void EnableManagers()
    {
        npcManager.enabled = true;
        scoreManager.enabled = true;
        weatherManager.enabled = true;
    }

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

    public void OnNotify(WeatherType weather)
    {

    }
    public void OnNotify(Action action)
    {

    }


}
