using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Emoji : MonoBehaviour
{
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;    
    }

    public void OnScale()
    {
        transform.DOScale(transform.localScale * 1.5f, 1f)
            .SetEase(Ease.InBounce)
            .OnComplete(() => 
            {
                transform.DOScale(_originalScale, 1f);
            });
    }
}
