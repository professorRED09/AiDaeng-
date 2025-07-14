using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NamwanInventory : MonoBehaviour
{
    [SerializeField] private Sprite[] _taskIcons;
    [SerializeField] private Image _taskIcon;
    private TMP_Text _serveAmountText;
    private float _drinkWaterServeAmount;
    private float _umbrellaServeAmount;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrinkWaterTask()
    {
        _taskIcon.sprite = _taskIcons[0];
    }

    public void OnUmbrellaTask()
    {
        _taskIcon.sprite = _taskIcons[1];
    }
}
