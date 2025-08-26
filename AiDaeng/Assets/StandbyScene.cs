using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StandbyScene : MonoBehaviour
{
    public void OnPressYesButtton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnPressNoButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
