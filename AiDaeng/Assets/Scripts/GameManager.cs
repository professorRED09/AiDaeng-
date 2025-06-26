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
    [SerializeField] TMP_Text bowlText;
    [SerializeField] GameObject StartNotif;
    [SerializeField] GameObject ClosedNotif;

    // Managers
    private NPC_Manager2 npcManager;
    private ScoreManager scoreManager;
    private WeatherManager weatherManager;

    [Header("Level timer")]
    [SerializeField] Timer timer; 

    public bool isGameover = false;

    private PlayerController1 player;
    private MusicManager musicManager;

    void Awake()
    {
        player = FindObjectOfType<PlayerController1>();
        npcManager = FindObjectOfType<NPC_Manager2>();
        scoreManager = FindObjectOfType<ScoreManager>();
        weatherManager = FindObjectOfType<WeatherManager>();
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void Start()
    {
        // start coroutine
        StartCoroutine(countdownToStart());
        // tell musicManager to play begin sound
        musicManager.StartCoroutine(musicManager.PlayBeginSound());
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
        musicManager.PlayResultMusic(scoreManager.StarCount());
        // if player's score is at least pass 1-star goal, inform them that they pass the level
        if (scoreManager.StarCount() >= 1)
        {
            resultText.text = "ผ่านด่านจ้า!";
            bowlText.text = $"คุณได้ {scoreManager.StarCount()} ถ้วย!";

            ////check player's score to give them bowl up to their score
            //if (scoreManager.StarCount() >= 3)
            //{
            //    bowlText.text = "คุณได้"
            //}
            //else if (scoreManager.StarCount() >= 2)
            //{

            //}
            //else
            //{

            //}
        }
        else
        {
            resultText.text = "ลองใหม่นะ";
            bowlText.text = "ว้าย ม่ายถ้วย";
        }        
        isGameover = true;
    }    

    public void OnNotify(WeatherType weather)
    {

    }
    public void OnNotify(Action action)
    {

    }


}
