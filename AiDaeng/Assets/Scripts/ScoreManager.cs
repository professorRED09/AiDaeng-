using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Ref")]
    //public Slider scoreBar;
    [SerializeField] private Image _scoreBarFill;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject scorePopupPrefab;
    [SerializeField] private GameObject[] stars;

    private NPC_Manager2 npcManager;
    public float baseScore = 1f;

    [Header("Setting")]
    public AudioClip addScoreSFX;   
    private AudioSource _audioSource;    
    [SerializeField] float _maxScore;
    [SerializeField] float _fillSpeed;
    [SerializeField] private float _currentScore;

    private void Awake()
    {
        npcManager = FindObjectOfType<NPC_Manager2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _maxScore = npcManager.maxTime * baseScore;
        _audioSource = GetComponent<AudioSource>();
        //initially set score to 0 when the game start
        _currentScore = 0;           
    }

    //private void Update()
    //{
    //    UpdateScoreBar(_currentScore, _maxScore);
    //}

    public void AddScore(Vector3 transform, float score)
    {
        GameObject m_scorePopup = Instantiate(scorePopupPrefab, new Vector2(transform.x + 1.2f, transform.y + 1.9f), Quaternion.identity);
        m_scorePopup.GetComponent<ScorePopup>().DisplayScoreText(score);

        _audioSource.clip = addScoreSFX;
        _audioSource.Play();

        _currentScore += score;
        UpdateScoreBar(_currentScore, _maxScore);       
        
    } 

    public void AddAllOrdersScore(Vector3 transform, float score)
    {
        GameObject m_scorePopup = Instantiate(scorePopupPrefab, new Vector2(transform.x + 1.2f, transform.y + 1.9f), Quaternion.identity);
        m_scorePopup.GetComponent<ScorePopup>().DisplayAllOrdersText(score);

        _audioSource.clip = addScoreSFX;
        _audioSource.Play();

        _currentScore += score;
        UpdateScoreBar(_currentScore, _maxScore);
    }

    public void UpdateScoreBar(float currentValue, float maxValue)
    {
        float targetFillAmount = _currentScore / _maxScore;
        _scoreBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        //scoreBar.value = currentValue / maxValue;
        scoreText.text = ((int)_currentScore).ToString();

        if (StarCount() >= 3)
        {
            stars[2].GetComponent<Animator>().SetTrigger("GetStar");
            Debug.Log("3 STARS");
        }
        else if (StarCount() >= 2)
        {
            stars[1].GetComponent<Animator>().SetTrigger("GetStar");
            Debug.Log("2 STARS");
        }
        else if (StarCount() >= 1)
        {
            stars[0].GetComponent<Animator>().SetTrigger("GetStar");
            Debug.Log("1 STAR");
        }
    }   

    // convert current score to number of star
    public float StarCount()
    {
        if(_currentScore >= _maxScore)
        {            
            return 3;
        }
        else if (_currentScore >= (2*_maxScore/3))
        {            
            return 2;            
        }
        else if (_currentScore >= (_maxScore/3))
        {            
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
