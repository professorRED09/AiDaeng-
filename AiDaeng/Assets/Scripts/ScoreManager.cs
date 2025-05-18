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
    [SerializeField] float goalScore;
    [SerializeField] float maxScore;
    public float currentScore;

    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;   //initially set score to 0 when the game start        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreBar(currentScore, maxScore);
    }

    public void AddScore()
    {
        currentScore += 1;
    }

    public void UpdateScoreBar(float currentValue, float maxValue)
    {
        scoreBar.value = currentValue / maxValue;
        scoreText.text = currentScore.ToString();
    }

    public bool DoesPlayerWin()
    {
        if(currentScore >= goalScore)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
