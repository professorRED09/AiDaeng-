using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComicCutscene : MonoBehaviour
{
    [SerializeField] private Image _cutsceneImage;
    [SerializeField] private Image _textImage;

    private void Awake()
    {
        _cutsceneImage.color = new Vector4(1f, 1f, 1f, 0f);
        _textImage.color = new Vector4(1f, 1f, 1f, 0f);
    }

    private void Start()
    {
        Enter();
    }

    public void Enter()
    {
        _cutsceneImage.DOFade(1f, 1f);
        _textImage.DOFade(1f, 2f);
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
    
}
