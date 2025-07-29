using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManu : MonoBehaviour
{
    [Header("UI")]
    //[SerializeField] GameObject startBanner;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingPanel;

    public void OnPressResumeGame()
    {
        StartCoroutine(OnResume());
    }
    public IEnumerator OnResume()
    {
        Time.timeScale = 1f;
        Debug.Log("Resume");
        pausePanel.GetComponent<Animator>().SetBool("isOpened", false);
        yield return new WaitForSeconds(1f);
        pausePanel.SetActive(false);
    }

    public void OnPressSetting()
    {
        settingPanel.SetActive(true);
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
}
