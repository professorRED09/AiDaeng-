using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Subject/*, IObserver*/
{
    [Header("UI")]
    [SerializeField] GameObject resultUI;
    [SerializeField] TMP_Text resultText;
    [SerializeField] Image resultPanel;
    [SerializeField] GameObject resultStar;
    [SerializeField] GameObject StartNotif;
    [SerializeField] GameObject ClosedNotif;

    public GameObject fireworksPrefab;
    [SerializeField] Sprite[] resultPanelSprites;

    // Managers
    private NPC_Manager2 npcManager;
    private ScoreManager scoreManager;
    private WeatherManager weatherManager;

    [Header("Level timer")]
    [SerializeField] Timer timer; 

    public bool isGameover = false;

    private PlayerController1 player;
    [SerializeField] private GameObject _namwan;
    [SerializeField] private GameObject _drinkWaterTask;
    [SerializeField] private GameObject _umbrellaTask;
    private MusicManager musicManager;

    private InteractiveTutorial tutorial;
    

    public enum Level
    {
        Level_1, Level_2, Level_3
    }

    [SerializeField] Level currentLevel;
    [Header("Highlight")]
    [SerializeField] GameObject[] _highlightedObjects;

    void Awake()
    {
        player = FindObjectOfType<PlayerController1>();
        npcManager = FindObjectOfType<NPC_Manager2>();
        scoreManager = FindObjectOfType<ScoreManager>();
        weatherManager = FindObjectOfType<WeatherManager>();
        musicManager = FindObjectOfType<MusicManager>();

        tutorial = FindObjectOfType<InteractiveTutorial>();
        //if (tutorial != null)
        //{
        //    tutorial.enabled = false;
        //}
    }

    private void Start()
    {
        // start coroutine
        StartCoroutine(CountdownToStart());
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

    void HighlightNewObject()
    {
        if(_highlightedObjects.Length > 0)
        {
            for(int i = 0; i < _highlightedObjects.Length; i++)
            {
                SpriteRenderer m_highlightObj = _highlightedObjects[i].GetComponent<SpriteRenderer>();
                m_highlightObj.DOColor(Color.gray, 0.5f).SetLoops(3,LoopType.Yoyo)
                    .OnComplete(()=> m_highlightObj.DOColor(Color.white, 0.5f));
                
            }
        }
    }

    IEnumerator CountdownToStart()
    {
        StartNotif.SetActive(true);
        StartNotif.GetComponent<Animator>().SetTrigger("Enter");    // play animation start pop up

        yield return new WaitForSeconds(3);                         // this make the game kinda jiggle, find a way to fix it later 
        StartNotif.GetComponent<Animator>().SetTrigger("Leave");
        timer.StartLevelTimer();

        EnableManagers();
        Invoke(nameof(HighlightNewObject), 0.5f);
        player.canControl = true;
    }    

    private void EnableManagers()
    {
        npcManager.enabled = true;
        scoreManager.enabled = true;

        if (currentLevel == Level.Level_1) return;
        weatherManager.enabled = true;
    }

    public void ShowResult()
    {        
        if (isGameover == true)
            return;
        resultUI.SetActive(true);
        musicManager.PlayResultMusic(scoreManager.StarCount());
        // if player's score is at least pass 1-star goal, inform them that they pass the level
        if (scoreManager.StarCount() >= 1)
        {
            resultText.text = "ผ่านด่านจ้า!";
            Instantiate(fireworksPrefab, transform);
            resultUI.transform.GetChild(0).GetComponent<Image>().color = new Vector4(1f,1f,1f,0.5f);
            resultPanel.sprite = resultPanelSprites[0];
        }
        else
        {
            resultText.text = "ลองใหม่นะ";
            resultUI.transform.GetChild(0).GetComponent<Image>().color = new Vector4(0.2f, 0.2f, 0.2f, 0.8f);
            resultPanel.sprite = resultPanelSprites[1];
        }
        ResultStar();
        isGameover = true;
    } 
    
    void ResultStar()
    {
        Animator m_Animator = resultStar.GetComponent<Animator>();
        if (scoreManager.StarCount() >= 3)
        {
            m_Animator.Play("3Stars");
        }
        else if (scoreManager.StarCount() >= 2)
        {
            m_Animator.Play("2Stars");
        }
        else if (scoreManager.StarCount() >= 1)
        {
            m_Animator.Play("1Star");
        }
        else
        {
            m_Animator.Play("Lose");
        }
       
    }  


}
