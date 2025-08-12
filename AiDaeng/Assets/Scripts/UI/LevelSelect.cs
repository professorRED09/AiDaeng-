using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] string selectedSceneName;
    [SerializeField] DaengLevelSelect daeng;

    [Header("Cutscene Name")]
    [SerializeField] string cutscene1Name;
    [SerializeField] string cutscene2Name;
    [SerializeField] string cutscene3Name;

    [Header("Level position")]
    [SerializeField] Transform level1Pos; 
    [SerializeField] Transform level2Pos;
    [SerializeField] Transform level3Pos;


    [Header("UI")]
    [SerializeField] TMP_Text levelNameText;

    private void Start()
    {
        OnSelectLevel1();
    }

    public void OnPressPlay()
    {
        if(selectedSceneName == null)
        {
            levelNameText.text = "-บอกให้เลือกด่านก่อนไง-";
            return;
        }
        SceneManager.LoadScene(selectedSceneName);
    }

    public void OnPressBack()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnSelectLevel1()
    {
        //change selectedSceneName to cutScene name
        if(cutscene1Name == null)
        {
            Debug.Log("Haven't added the scene yet");
            return;
        }
        selectedSceneName = cutscene1Name;
        levelNameText.text = "-ด่านที่ 1-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level1Pos.TransformPoint(level2Pos.position)); ;
    }

    public void OnSelectLevel2()
    {
        if (cutscene2Name == null)
        {
            Debug.Log("Haven't added the scene yet");
            return;
        }
        //change selectedSceneName to cutscene name
        selectedSceneName = cutscene2Name;
        levelNameText.text = "-ด่านที่ 2-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level2Pos.TransformPoint(level2Pos.position));
    }

    public void OnSelectLevel3()
    {
        if (cutscene3Name == null)
        {
            Debug.Log("Haven't added the scene yet");
            return;
        }
        //change selectedSceneName to cutscene name
        selectedSceneName = cutscene3Name;
        levelNameText.text = "-ด่านที่ 3-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level3Pos.TransformPoint(level2Pos.position));
    }
}
