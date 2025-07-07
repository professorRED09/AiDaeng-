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
    [SerializeField] Image _transitionScreen;

    public void LoadScene(string sceneName)
    {
        _transitionScreen.DOFade(1f, 1f).OnComplete(() => StartCoroutine(LoadSceneAsync(sceneName)));
        //StartCoroutine(LoadSceneAsync(sceneName));

        //_transitionScreen.DOFade(1f, 1f).OnComplete(() => 
        //_transitionScreen.DOFade(0f, 1f).OnComplete(() => StartCoroutine(LoadSceneAsync(sceneName))));
    }    

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        _loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingBarFill.fillAmount = progressValue;
            //_loadingBarFill.DOFillAmount(progressValue, 1f);
            yield return null;
        }

    }
}
