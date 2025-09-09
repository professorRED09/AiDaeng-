using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using DG.Tweening;
using System;

public class ComicCutscene : MonoBehaviour
{
    public VideoClip[] cutsceneVideo;
    private VideoPlayer _videoPlayer;
    [SerializeField] private int _currentIndex;

    [Header("UI Button")]
    public GameObject nextButton;
    public GameObject previousButton;
    public GameObject goButton;

    public GameObject hoverPanel;
    public TMP_Text mouseToBottomBorderText; 

    [Header("Holding To Skip")]
    public GameObject holdingUI;
    public Image holdingFillImage;

    public float holdDuration = 0.6f; 
    private bool _isHolding = false;
    private float _holdTimer = 0f;

    [Header("Load Scene")]
    public LoadingScene asyncManager;
    public string dialogueSceneName;

    [Header("Pause UI")]
    public GameObject pauseOnCutsceneUI;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        _currentIndex = 0;
        PlayCutscene(0);
    }   

    void Update()
    {
        //holding to skip function
        if (_isHolding)
        {
            _holdTimer += Time.deltaTime;
            holdingFillImage.fillAmount = _holdTimer / holdDuration;

            if (_holdTimer >= holdDuration)
            {
                holdingUI.SetActive(false);
                _isHolding = false;
                PlayCutscene(1);
                //asyncManager.LoadScene("CCutscene1");
            }            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseOnCutsceneUI.SetActive(true);
        }
    }

    public void OnHoldingSkipButton()
    {
        _isHolding = true;
        _holdTimer = 0;
        holdingUI.SetActive(true);
        //Debug.Log("Button Pressed Down!");
    }

    public void OnReleasingSkipButton()
    {
        _isHolding = false;
        holdingUI.SetActive(false);
        //Debug.Log("Button Released!");
    }

    public void OnMouseEnterBottomBorder()
    {
        //Debug.Log("Mouse enter");
        if (_currentIndex <= 0)
        {
            //print("VideoEND");
            nextButton.SetActive(true);
            previousButton.SetActive(false);
            goButton.SetActive(false);
        }
        else if (_currentIndex >= cutsceneVideo.Length - 1)
        {
            //print("VideoEND2");
            nextButton.SetActive(false);
            previousButton.SetActive(true);
            goButton.SetActive(true);
        }
        else
        {
            //print("VideoEND3");
            nextButton.SetActive(true);
            previousButton.SetActive(true);
            goButton.SetActive(false);
        }
    }

    public void OnMouseLeftBottomBorder()
    {
        //Debug.Log("Mouse left");
        nextButton.SetActive(false);
        previousButton.SetActive(false);
        goButton.SetActive(false);
    }


    public void PlayCutscene(int m_indexOffset)
    {
        _currentIndex += m_indexOffset;

        if (_currentIndex >= cutsceneVideo.Length)
        {
            asyncManager.LoadNextScene();
            return;
        }

        _videoPlayer.clip = cutsceneVideo[_currentIndex];

        mouseToBottomBorderText.DOFade(0f, 0f);

        nextButton.SetActive(false);
        previousButton.SetActive(false);
        goButton.SetActive(false);

        hoverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if(_videoPlayer != null)
        {
            _videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private void OnDisable()
    {
        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    private void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        hoverPanel.SetActive(true);
        mouseToBottomBorderText.DOFade(1f, 0.45f)
            .OnComplete(() => mouseToBottomBorderText.DOFade(0f, 5f));
        
        //if (_currentIndex <= 0)
        //{
        //    print("VideoEND");
        //    nextButton.SetActive(true);
        //    previousButton.SetActive(false);
        //    goButton.SetActive(false);
        //}
        //else if (_currentIndex >= cutsceneVideo.Length - 1) 
        //{
        //    print("VideoEND2");
        //    nextButton.SetActive(false);
        //    previousButton.SetActive(true);
        //    goButton.SetActive(true);
        //}
        //else
        //{
        //    print("VideoEND3");
        //    nextButton.SetActive(true);
        //    previousButton.SetActive(true);
        //    goButton.SetActive(false);
        //}
    }

}