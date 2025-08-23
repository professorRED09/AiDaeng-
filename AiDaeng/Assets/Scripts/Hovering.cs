using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image _buttonImage;
    private Sprite _originalButtonSprite;
    [SerializeField] private Sprite _hoverButtonSprite;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>(); 
    }

    private void Start()
    {
        _originalButtonSprite = _buttonImage.sprite;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _buttonImage.sprite = _hoverButtonSprite;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _buttonImage.sprite = _originalButtonSprite;
    }

    //private void OnMouseOver()
    //{
    //    _buttonImage.sprite = _hoverButtonSprite;
    //    print("CHANGE TO NEW");
    //}

    //private void OnMouseExit()
    //{
    //    _buttonImage.sprite = _originalButtonSprite;
    //    print("CHANGE TO OLD");
    //}

    //private void OnMouseOver()
    //{
    //    _buttonImage.sprite = _hoverButtonSprite;
    //    print("CHANGE TO NEW");
    //}

    //private void OnMouseExit()
    //{
    //    _buttonImage.sprite = _originalButtonSprite;
    //    print("CHANGE TO OLD");
    //}



}
