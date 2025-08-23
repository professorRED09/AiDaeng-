using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class IntroVideo : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    [SerializeField] private Image _transitionImg;

    // Start is called before the first frame update
    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();        
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.anyKeyDown)
        {            
            StartCoroutine(TransitionToTitle(0.5f));
            //SceneManager.LoadScene("Title");
            print("MU HA HA");
        }
    }

    IEnumerator TransitionToTitle(float time)
    {
        _transitionImg.DOFade(1f, time);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Title");
    }

    void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        StartCoroutine(TransitionToTitle(0.5f));
        //SceneManager.LoadScene("Title");        
    }

    void OnEnable()
    {
        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            print("NONONON");
        }
    }

    void OnDisable()
    {
        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
