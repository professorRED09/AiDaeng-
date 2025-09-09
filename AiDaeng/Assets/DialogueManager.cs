using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public Animator[] charactersArray;

    public AnimationClip[] daengAnimation;
    public AnimationClip[] jaoawadAnimation;
    public AnimationClip[] namwanAnimation;

    [TextArea(3, 10)]
    public string[] sentences;

    public string[] names;
    public bool[] isQuestion;
    public bool[] isSkipToOtherIndex;
    public bool[] isSystemQuestion;
    public int[] indexToSkipTo;
    public float textDelayTime;

    [Header("Variable")]
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public TMP_Text continueText;
    public GameObject q1Choices;
    //public TMP_Text textContinue;

    [SerializeField] private bool _isTyping = false;
    private int _currentIndex;

    [Header("System Question")]
    public GameObject[] systemQuestions;
    public Queue<GameObject> systemQuestionsQueue = new Queue<GameObject>();

    [Header("LoadScene")]
    public LoadingScene asyncManager;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
        BlinkingText("คลิกเพื่อไปต่อ");

        for(int i = 0; i < systemQuestions.Length; i++)
        {
            systemQuestionsQueue.Enqueue(systemQuestions[i]);
        }
            
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
        continueText.text = m_text;
       /* _blinkTween = */DOTween.Sequence()
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

        BlinkingText("คลิกเพื่อไปต่อ");

        UpdateAnimation();
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
            q1Choices.SetActive(true);
            BlinkingText("คลิกเลือกคำตอบเพื่อไปต่อ");
            Debug.Log("This is a question 1");
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
        else if (isSkipToOtherIndex[_currentIndex] == true)
        {
            _currentIndex = indexToSkipTo[_currentIndex] - 1;
        }
        else if (isSystemQuestion[_currentIndex] == true)
        {
            ShowUIBox(systemQuestionsQueue.Dequeue());
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
            asyncManager.LoadNextScene();
        }
        else
        {            
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
    
    public void OnPressNoToSystemQuestion()
    {
        for(int i = 0; i < systemQuestions.Length; i++)
        {
            systemQuestions[i].SetActive(false);
        }
        _currentIndex = 7;
        dialogueText.text = sentences[_currentIndex];
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
        
    }

    public void OnPressSkip()
    {
        if(_currentIndex >= 7 || systemQuestionsQueue.Count <= 0)
        {
            asyncManager.LoadNextScene();
        }
        else if(_currentIndex < 7 && systemQuestionsQueue.Count > 0)
        {
            ShowUIBox(systemQuestionsQueue.Dequeue());
        }
    }

    public void UpdateAnimation()
    {
        for(int i = 0; i < charactersArray.Length; i++)
        {
            if(charactersArray[i].gameObject.name == "แดง" && daengAnimation[_currentIndex] != null)
            {
                charactersArray[i].Play(daengAnimation[_currentIndex].name);
            }
            else if (charactersArray[i].gameObject.name == "เจ้าอาวาส" && jaoawadAnimation[_currentIndex] != null)
            {
                charactersArray[i].Play(jaoawadAnimation[_currentIndex].name);
            }
            else if (charactersArray[i].gameObject.name == "น้ำหวาน" && namwanAnimation[_currentIndex] != null)
            {
                charactersArray[i].Play(namwanAnimation[_currentIndex].name);
            }
        }
    }

    public void ShowUIBox(GameObject m_UIBox)
    {
        m_UIBox.SetActive(true);
        m_UIBox.transform.GetChild(0).GetComponent<Image>().DOFade(0.7f, 0.5f).OnComplete(() =>
          m_UIBox.transform.GetChild(1).gameObject.transform.DOLocalMoveX(0, 1f));
    }


}
