using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene & UI")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject tutorialPanel;

    public void OnPressStartGame()
    {
        SceneManager.LoadScene("Cutscene1");
    }

    public void OnPressSetting()
    {
        settingPanel.SetActive(true);
    }

    public void OnPressTutorial()
    {
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
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnPressResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPressLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnPressSkipScene1()
    {
        SceneManager.LoadScene("Gameplay1");
    }

    public void OnPressMainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnPressBackFromLevelSelect()
    {
        SceneManager.LoadScene("Title");
    }
}
