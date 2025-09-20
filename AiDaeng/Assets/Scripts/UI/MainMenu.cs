using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] string cutscene1Name;
    [SerializeField] string level1Name;

    [Header("UI")]
    [SerializeField] GameObject levelSelectUI;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject tutorialPanel;

    [SerializeField] Transform logoImage;
    //[SerializeField] GameObject _productTutorialPanel;

    private void Start()
    {
        Time.timeScale = 1f;

        if (logoImage != null)
        {
            logoImage.DOScale(logoImage.localScale * 0.95f, 1f).SetEase(Ease.InQuad).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressPauseGame();
        }
    }

    //public void OnProductTutorialPress()
    //{
    //    Time.timeScale = 0f;
    //    _productTutorialPanel.SetActive(true);
    //}

    public void OnPressStartGame()
    {
        SceneManager.LoadScene(cutscene1Name);
    }

    public void OnPressSetting()
    {
        settingPanel.SetActive(true);
    }

    public void OnPressTutorial()
    {
        Time.timeScale = 0f;
        tutorialPanel.SetActive(true);
    }

    public void OnPressBackFromSetting()
    {
        settingPanel.SetActive(false);
    }

    public void OnPressBackFromTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void OnPressPauseGame()
    {
        Debug.Log("Pause");
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animator>().SetBool("isOpened", true);
        Time.timeScale = 0f;
    }

    //public void OnPressResumeGame()
    //{
    //    StartCoroutine(OnResume());
    //}

    public void OnPressLevelSelect()
    {
        levelSelectUI.SetActive(true);
    }

    public void OnPressSkipScene1()
    {
        SceneManager.LoadScene(level1Name);
    }

    public void OnPressRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPressMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    public void OnPressBackFromLevelSelect()
    {
        SceneManager.LoadScene("Title");
    }





}
