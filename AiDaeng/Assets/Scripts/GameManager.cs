using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject resultPanel;
    public GameObject pausePanel;
    public GameObject settingPanel;
    public GameObject tutorialPanel;
    public TMP_Text resultText;

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
        resultPanel.SetActive(true);
        if (ScoreManager.instance.DoesPlayerWin() == true)
        {
            resultText.text = "ผ่านด่าน!";
        }
        else
        {
            resultText.text = "ลองเล่นใหม่!";
        }

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Cutscene1");
    }

    public void ShowSetting()
    {
        settingPanel.SetActive(true);
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void BackFromSetting()
    {
        settingPanel.SetActive(false);
    }

    public void BackFromTutorial()
    {
        tutorialPanel.SetActive(false);
    }


    public void PauseGame()
    {
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
    }

    public void ShowLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void BackFromLevelSelect()
    {
        SceneManager.LoadScene("Title");
    }


}
