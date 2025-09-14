using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ComicCutsceneGuide : MonoBehaviour
{
    public ComicCutscene comicCutscene;
    //public Image guideImage;
    //public TMP_Text narrativeText;
    [SerializeField] Sprite[] _guideSprites;
    [TextArea(3, 10)]
    [SerializeField] string[] _guideTexts;
    [SerializeField] private int _currentIndex = 0;

    public GameObject nextButton;
    public GameObject previousButton;
    public GameObject goButton;

    //[Header("UI Window")]
    //public GameObject guideUI;
    //public GameObject UIBox;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _currentIndex = 0;
        GoToNextGuide(0);
    }

    //void StartInstructor()
    //{
    //    ShowUIBox(instructorUI);
    //    _currentIndex = 0;
    //    Instruct(0);
    //}

    public void GoToNextGuide(int m_indexOffset)
    {
        _currentIndex += m_indexOffset;
        print(_currentIndex);

        this.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = _guideSprites[_currentIndex];
        this.gameObject.transform.GetChild(3).GetComponent<TMP_Text>().text = _guideTexts[_currentIndex];

        if (_currentIndex >= _guideSprites.Length - 1)
        {
            nextButton.SetActive(false);
            previousButton.SetActive(true);
            goButton.SetActive(true);
        }
        else if (_currentIndex <= 0)
        {
            nextButton.SetActive(true);
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

    public void EndGuide()
    {
        this.gameObject.SetActive(false);
        comicCutscene.enabled = true;
    }

    //public void OnBackButton()
    //{
    //    Invoke(nameof(HideInstructorUI), 0.5f);
    //    DOTween.Sequence()
    //        .Append(instructorUI.transform.GetChild(1).gameObject.transform.DOLocalMoveX(1713, 1f))
    //        .Append(instructorUI.transform.GetChild(0).GetComponent<Image>().DOFade(0f, 0.5f))
    //        .SetUpdate(true)
    //        .OnComplete(() => Time.timeScale = 1);
    //}

    //void HideInstructorUI()
    //{
    //    instructorUI.SetActive(false);
    //}
}
