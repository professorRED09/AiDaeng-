using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    public TMP_Text scoreText;
    
    public void DisplayScoreText(float score)
    {
        scoreText.text = $"+ {score} แต้ม!";
    }

    public void DisplayAllOrdersText(float score)
    {
        scoreText.text = $"+ โบนัส {score} แต้ม!";
    }
}
