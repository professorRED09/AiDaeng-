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
    public GameObject scorePopupPrefab;
    public GameObject[] stars;

    [Header("Setting")]
    public AudioClip addScoreSFX;
    //[SerializeField] float oneStarScore;
    //[SerializeField] float twoStarsScore;    
    //[SerializeField] float threeStarsScore;    
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

    public void AddScore(Vector3 transform)
    {
        GameObject m_scorePopup = Instantiate(scorePopupPrefab, new Vector2(transform.x + 1.2f, transform.y + 1.9f), Quaternion.identity);
        m_scorePopup.GetComponent<ScorePopup>().DisplayScoreText(1);

        audioSource.clip = addScoreSFX;
        audioSource.Play();

        currentScore += 1;
        UpdateScoreBar(currentScore, maxScore);       
        
    }

    

    

    public void UpdateScoreBar(float currentValue, float maxValue)
    {
        scoreBar.value = currentValue / maxValue;
        scoreText.text = currentScore.ToString();

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
        if(currentScore >= maxScore)
        {            
            return 3;
        }
        else if (currentScore >= (2*maxScore/3))
        {            
            return 2;            
        }
        else if (currentScore >= (maxScore/3))
        {            
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
