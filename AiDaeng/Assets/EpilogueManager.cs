using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Video;
using TMPro;

public class EpilogueManager : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] sentences;
    public Sprite[] sprites;
    public bool[] changeSpriteThisIndex;
    public float textDelayTime;    

    public Image stillImage;

    public Image transitionImage;
    public Image endImage;
    public RawImage projector;
    public VideoPlayer videoPlayer;

    public TMP_Text dialogueText;
    public TMP_Text continueText;

    [SerializeField] private bool _isTyping = false;
    [SerializeField] private int _currentIndex;

    [Header("LoadScene")]
    public LoadingScene asyncManager;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
        BlinkingText("คลิกเพื่อไปต่อ");
    }

    private void StartDialogue()
    {
        _currentIndex = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(TypeSentence());
    }

    private void BlinkingText(string m_text)
    {
        continueText.gameObject.SetActive(true);
        continueText.text = m_text;
        DOTween.Sequence()
            .Append(continueText.DOFade(0f, 1f).SetEase(Ease.Linear))
            .Append(continueText.DOFade(1f, 1f).SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Restart);
    }

    void TransitionToVideo()
    {
        transitionImage.DOFade(1f, 1f);
        Invoke(nameof(EnableProjectorAndVideo), 1.5f);
    }

    void EnableProjectorAndVideo()
    {
        RenderTexture.active = videoPlayer.targetTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;

        projector.enabled = true;
        projector.DOFade(1f, 0.5f);
        videoPlayer.enabled = true;
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = string.Empty;       

        BlinkingText("คลิกเพื่อไปต่อ");

        _isTyping = true;

        int maxLength = sentences[_currentIndex].Length;

        for (int i = 0; i < maxLength; i++)
        {
            if (i < sentences[_currentIndex].Length)
            {
                dialogueText.text += sentences[_currentIndex][i];
            }
            yield return new WaitForSeconds(textDelayTime);
        }

        _isTyping = false;
        //BlinkingText("คลิกเพื่อไปต่อ");
    }

    public void OnPressNextSentenceButton()
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = sentences[_currentIndex];
            _isTyping = false;
            //print("FULL");
            return;
        }

        if (changeSpriteThisIndex[_currentIndex] == true)
        {
            stillImage.sprite = sprites[_currentIndex];
        }
        else
        {

        }

        _currentIndex++;
        //Debug.Log(_currentIndex);

        if (_currentIndex >= sentences.Length)
        {
            TransitionToVideo();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence());
        }
    }

    private void OnEnable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private void OnDisable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    private void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        endImage.gameObject.SetActive(true);
        endImage.DOFade(1f, 1f)
            .OnComplete(() => endImage.transform.GetChild(0)
            .GetComponent<TMP_Text>().enabled = true);
        Invoke(nameof(BackToIntro), 4f);
    }

    void BackToIntro()
    {
        asyncManager.LoadScene("Intro");
    }
}
