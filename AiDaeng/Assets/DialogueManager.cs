using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public Animator[] charactersArray;    

    [TextArea(3, 10)]
    public string[] sentences;
    public string[] names;
    public bool[] isQuestion;
    public float textDelayTime;

    [Header("Variable")]
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public TMP_Text continueText;
    public GameObject q1Choices;
    //public TMP_Text textContinue;

    [SerializeField] private bool _isTyping = false;
    private int _currentIndex;
    private Tween _blinkTween;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
        BlinkingText("คลิกเพื่อไปต่อ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartDialogue()
    {
        _currentIndex = 0;
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;

        StartCoroutine(TypeSentence());
    }

    private void BlinkingText(string m_text)
    {
        continueText.gameObject.SetActive(true);
        _blinkTween = DOTween.Sequence()
            .Append(continueText.DOFade(0f, 1f).SetEase(Ease.Linear))
            .Append(continueText.DOFade(1f, 1f).SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Restart);

    }
        

    private void MakeSpeakerStandOut()   
    {
        for(int i = 0; i < charactersArray.Length; i++)
        {
            if (names[_currentIndex] == charactersArray[i].gameObject.name)
            {
                charactersArray[i].gameObject.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 1f);
            }
            else
            {
                charactersArray[i].gameObject.GetComponent<Image>().color = new Vector4(0.48f, 0.53f, 0.76f, 1f);
            }
        }
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
        nameText.text = names[_currentIndex];

        MakeSpeakerStandOut();

        _isTyping = true;

        int maxLength = Mathf.Max(names[_currentIndex].Length, sentences[_currentIndex].Length);

        for (int i = 0; i < maxLength; i++)
        {
            if (i < sentences[_currentIndex].Length)
            {
                dialogueText.text += sentences[_currentIndex][i];
            }
            yield return new WaitForSeconds(textDelayTime);
        }

        if(isQuestion[_currentIndex] == true)
        {
            Debug.Log("This is a question");
            q1Choices.SetActive(true);
        }

        _isTyping = false;
        //BlinkingText("คลิกเพื่อไปต่อ");
    }

    public void OnPressNextSentenceButton()
    {
        // if current sentence is question, player can't press to next button until they answer it
        if (isQuestion[_currentIndex] == true)
        {
            print("This is a question, please answer it dammit");
            return;
        }
        else if (_isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = sentences[_currentIndex];
            nameText.text = names[_currentIndex];
            _isTyping = false;
            
            return;
        }

        _currentIndex++;
        Debug.Log(_currentIndex);

        if (_currentIndex >= sentences.Length)
        {            
            return;
        }
        else
        {
            dialogueText.text = sentences[_currentIndex];
            StopAllCoroutines();
            StartCoroutine(TypeSentence());
        }
    }

    public void OnPressAnswerOnQ1(int m_goToIndex)
    {
        _currentIndex = m_goToIndex;
        dialogueText.text = sentences[_currentIndex];
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
        q1Choices.SetActive(false);
    }
    
    //public void OnPressNoOnQ1()
    //{
    //    _currentIndex++;
    //    dialogueText.text = sentences[_currentIndex];
    //    StopAllCoroutines();
    //    StartCoroutine(TypeSentence());
    //}

}
