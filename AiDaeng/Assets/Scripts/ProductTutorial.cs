using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTutorial : MonoBehaviour
{
    [SerializeField] GameObject _productTutorialPanel;

    private void OnMouseDown()
    {
        Time.timeScale = 0f;
        _productTutorialPanel.SetActive(true);
    }
}
