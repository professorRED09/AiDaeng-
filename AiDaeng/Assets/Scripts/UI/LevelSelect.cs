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

    //void Awake()
    //{
    //    _defaultPlayButtonScale = playButtonTransform.localScale;
    //}

    private void Start()
    {
        OnSelectLevel1();
        playButtonTransform.DOScale(transform.localScale *= _scaleValue, _scaleDuration)
            .SetLoops(-1,LoopType.Yoyo);
    }


    //public void OnPressBack()
    //{
    //    SceneManager.LoadScene("Title");
    //}

    public void OnSelectLevel1()
    {  
        levelNameText.text = "-ด่านที่ 1-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level1Pos); ;
        asyncManager.SelectSceneToPlay("CCutscene1");
    }

    public void OnSelectLevel2()
    {
        levelNameText.text = "-ด่านที่ 2-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level2Pos);
        asyncManager.SelectSceneToPlay("CutsceneComic2");
    }

    public void OnSelectLevel3()
    {
        levelNameText.text = "-ด่านที่ 3-";

        //Daeng walk to the selected cutscene
        daeng.SetWaypoint(level3Pos);
        asyncManager.SelectSceneToPlay("CutsceneComic3");
    }
}
