using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using DG.Tweening;

public class Instructor : MonoBehaviour
{
    public List<InstructorPage> instructorPages;
    //private InstructorPage _currentPage;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private int _currentIndex = 0;
    

    public GameObject nextButton;
    public GameObject previousButton;
    public GameObject goButton;

    [Header("UI Window")]
    public GameObject instructorUI;
    public GameObject UIBox;

    private void Awake()
    {
        _videoPlayer = FindObjectOfType<VideoPlayer>();        
    }

    private void Start()
    {        
        Invoke(nameof(StartInstructor), 3.5f);
    }

    void StartInstructor()
    {
        ShowUIBox(instructorUI);
        _currentIndex = 0;
        Instruct(0);
    }

    public void Instruct(int m_indexOffset)
    {
        _currentIndex += m_indexOffset;
        print(_currentIndex);

        _videoPlayer.clip = instructorPages[_currentIndex].videoClip;
        UIBox.transform.GetChild(1).GetComponent<TMP_Text>().text = instructorPages[_currentIndex].topicText;
        UIBox.transform.GetChild(2).GetComponent<TMP_Text>().text = instructorPages[_currentIndex].narrativeText;

        if (_currentIndex >= instructorPages.Count - 1)
        {
            nextButton.SetActive(false);
            goButton.SetActive(true);
        }
        else if (_currentIndex <= 0)
        {
            previousButton.SetActive(false);
            goButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            previousButton.SetActive(true);
            goButton.SetActive(false);
        }
    }

    public void ShowUIBox(GameObject m_UIBox)
    {
        Time.timeScale = 0;
        m_UIBox.SetActive(true);
        //m_UIBox.transform.GetChild(0).GetComponent<Image>().DOFade(0.7f, 0.5f)
        //    .OnComplete(() => m_UIBox.transform.GetChild(1).gameObject.transform.DOLocalMoveX(0, 1f))
        //    .SetUpdate(true);
        DOTween.Sequence()
            .Append(m_UIBox.transform.GetChild(0).GetComponent<Image>().DOFade(0.7f, 0.5f))
            .Append(m_UIBox.transform.GetChild(1).gameObject.transform.DOLocalMoveX(0, 1f))
            .SetUpdate(true);
    }

    public void OnBackButton()
    {       
        DOTween.Sequence()
            .Append(instructorUI.transform.GetChild(1).gameObject.transform.DOLocalMoveX(1713, 1f))
            .Append(instructorUI.transform.GetChild(0).GetComponent<Image>().DOFade(0f, 0.5f))
            .SetUpdate(true)
            .OnComplete(() => Time.timeScale = 1);
    }
}
