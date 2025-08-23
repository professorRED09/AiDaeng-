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
    //[SerializeField] Image _transitionScreen;

    public void LoadScene(string sceneName)
    {
        //_transitionScreen.DOFade(1f, 1f).OnComplete(() => StartCoroutine(LoadSceneAsync(sceneName)));
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
        

        //_transitionScreen.DOFade(1f, 1f).OnComplete(() => 
        //_transitionScreen.DOFade(0f, 1f).OnComplete(() => StartCoroutine(LoadSceneAsync(sceneName))));
    }    

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        //operation.allowSceneActivation = false;        

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingBarFill.fillAmount = progressValue;

            //if (operation.progress >= 0.9f)
            //{
            //    operation.allowSceneActivation = true;

            //    //if (Input.GetKey(KeyCode.Space))
            //    //{
            //    //    print("ASD");
            //    //    operation.allowSceneActivation = true;
            //    //}
            //}

            yield return null;
        }        
    }
}
