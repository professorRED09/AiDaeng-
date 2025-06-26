using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Ref")]
    public Slider scoreBar;
    public TMP_Text scoreText;

    [Header("Setting")]
    public AudioClip addScoreSFX;
    [SerializeField] float oneStarScore;
    [SerializeField] float twoStarsScore;    
    [SerializeField] float threeStarsScore;    
    private AudioSource audioSource;
    
    [SerializeField] float maxScore;
    public float currentScore;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentScore = 0;   //initially set score to 0 when the game start        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreBar(currentScore, maxScore);
    }

    public void AddScore()
    {
        audioSource.clip = addScoreSFX;
        audioSource.Play();
        currentScore += 1;
    }

    public void UpdateScoreBar(float currentValue, float maxValue)
    {
        scoreBar.value = currentValue / maxValue;
        scoreText.text = currentScore.ToString();
    }

    // convert current score to number of star
    public float StarCount()
    {
        if(currentScore >= threeStarsScore)
        {
            return 3;
        }
        else if (currentScore >= twoStarsScore)
        {
            return 2;
        }
        else if (currentScore >= oneStarScore)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
