using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StandbyScene : MonoBehaviour
{
    
    public void OnPressYesButtton()
    {
        SceneManager.LoadScene("Gameplay Tutorial");
    }

    public void OnPressNoButton()
    {
        SceneManager.LoadScene("Gameplay Level 1");
    }
}
