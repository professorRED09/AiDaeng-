using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
//using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public LoadingScene asyncManager;
    [SerializeField] DaengLevelSelect daeng;

    [Header("Level position")]
    [SerializeField] Transform level1Pos; 
    [SerializeField] Transform level2Pos;
    [SerializeField] Transform level3Pos;


    [Header("UI")]
    [SerializeField] TMP_Text levelNameText;

    public Transform playButtonTransform;
    private Vector3 _defaultPlayButtonScale;

    [SerializeField] float _scaleValue;
    [SerializeField] float _scaleDuration;

    [Header("Do Pass")]
    [SerializeField] bool doPassLv1;
    [SerializeField] bool doPassLv2;


    private void Start()
    {
        OnSelectLevel1();
        playButtonTransform.DOScale(transform.localScale *= _scaleValue, _scaleDuration)
            .SetLoops(-1,LoopType.Yoyo);

        
        //if(PlayerPrefs.GetInt("Level1Passed") == 1)
        //{
        //    doPassLv1 = true;
        //}
        //else if (PlayerPrefs.GetInt("Level2Passed") == 1) 
        //{
        //    doPassLv2 = true;
        //}
    }

    public void OnPressBack()
    {
        gameObject.SetActive(false);
    }

    public void OnSelectLevel1()
    {  
        
        levelNameText.text = "-ด่านที่ 1-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level1Pos); ;
        asyncManager.SelectSceneToPlay("CCutscene0");
    }

    public void OnSelectLevel2()
    {
        //if (!doPassLv1) return;
        levelNameText.text = "-ด่านที่ 2-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level2Pos);
        asyncManager.SelectSceneToPlay("CCutscene2 Part 1");
    }

    public void OnSelectLevel3()
    {
        //if (!doPassLv2) return;
        levelNameText.text = "-ด่านที่ 3-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level3Pos);
        asyncManager.SelectSceneToPlay("CutsceneComic3");
    }
}
