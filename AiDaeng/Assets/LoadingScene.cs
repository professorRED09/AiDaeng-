using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Image _loadingBarFill;
    [SerializeField] string _selectedSceneName;
    //[SerializeField] Image _transitionScreen;

    public void LoadScene(string sceneName)
    {
        //_transitionScreen.DOFade(1f, 1f).OnComplete(() => StartCoroutine(LoadSceneAsync(sceneName)));
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }   
    
    public void SelectSceneToPlay(string m_sceneName)
    {
        _selectedSceneName = m_sceneName;
    }

    public void LoadSceneSelected()
    {
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(_selectedSceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        //operation.allowSceneActivation = false;        

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingBarFill.fillAmount = progressValue;

            yield return null;
        }        
    }

    public void LoadNextScene()
    {
        //_transitionScreen.DOFade(1f, 1f).OnComplete(() => StartCoroutine(LoadSceneAsync(sceneName)));
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //operation.allowSceneActivation = false;        

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

    public void OnPressQuitGame()
    {
        Application.Quit();
    }
}
