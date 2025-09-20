using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using DG.Tweening;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Image _loadingBarFill;
    [SerializeField] string _selectedSceneName;

    [Header("Trivia")]
    [SerializeField] SongkranTrivia[] _trivias;
    [SerializeField] SongkranTrivia _trivia;

    [Header("Trivia UI")]
    [SerializeField] TMP_Text _topicText;
    [SerializeField] TMP_Text _narrativeText;
    [SerializeField] Image _triviaImage;

    public void LoadScene(string sceneName)
    {
        if(_trivias.Length > 0)
        {
            _trivia = _trivias[Random.Range(0, _trivias.Length)];
            _topicText.text = _trivia.topicText;
            _narrativeText.text = _trivia.narrativeText;
            _triviaImage.sprite = _trivia.sprite;

        }

        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }   
    
    public void SelectSceneToPlay(string m_sceneName)
    {
        _selectedSceneName = m_sceneName;
    }

    public void LoadSceneSelected()
    {
        if (_trivias.Length > 0)
        {
            _trivia = _trivias[Random.Range(0, _trivias.Length)];
            _topicText.text = _trivia.topicText;
            _narrativeText.text = _trivia.narrativeText;
            _triviaImage.sprite = _trivia.sprite;

        }

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

    public void LoadSceneSudden(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        if (_trivias.Length > 0)
        {
            _trivia = _trivias[Random.Range(0, _trivias.Length)];
            _topicText.text = _trivia.topicText;
            _narrativeText.text = _trivia.narrativeText;
            _triviaImage.sprite = _trivia.sprite;

        }

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
