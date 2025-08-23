using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Transition : MonoBehaviour
{
    private Image _transitionImg;

    private void Awake()
    {
        _transitionImg = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _transitionImg.enabled = true;
        _transitionImg.DOFade(0f, 1f);
        _transitionImg.raycastTarget = false;
    }

    
}
