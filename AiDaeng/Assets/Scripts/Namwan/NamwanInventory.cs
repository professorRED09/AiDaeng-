using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NamwanInventory : MonoBehaviour
{   
    [Header("UI Ref")]
    [SerializeField] private Sprite[] _taskIcons;
    [SerializeField] private Image _taskIcon;
    [SerializeField] private TMP_Text _serveAmountText;    
    public float serveAmount;

    [Header("Settings")]
    [SerializeField] float _maxDrinkWaterServeAmount;
    //[SerializeField] float _maxUmbrellaServeAmount;
    [SerializeField] private float _drinkWaterServeAmount;
    [SerializeField] private float _umbrellaServeAmount;

    public bool canServe = true;
    [SerializeField] private UmbrellaTask _umbrellaTask;


    // Start is called before the first frame update
    void Start()
    {
        _drinkWaterServeAmount = _maxDrinkWaterServeAmount;           
        UpdateOnServeAmountUI(_drinkWaterServeAmount);
    }

    private void Update()
    {
        if (serveAmount < 1)
        {
            canServe = false;
        }
        else
        {
            canServe = true;
        }
    }


    public void TakeUmbrella(UmbrellaTask m_task)
    {        
        if (m_task.umbrellaAmount > 0)
        {
            _umbrellaServeAmount += m_task.umbrellaAmount;
            m_task.umbrellaAmount = 0;
            m_task.UpdateOnUIText();

            UpdateOnServeAmountUI(_umbrellaServeAmount);
        }
    }

    public void DecreaseUmbrella()
    {
        _umbrellaServeAmount -= 1;
        UpdateOnServeAmountUI(_umbrellaServeAmount);
    }

    public void DeductDrinkWater()
    {
        _drinkWaterServeAmount -= 1;
        UpdateOnServeAmountUI(_drinkWaterServeAmount);
    }

    void UpdateOnServeAmountUI(float taskServeAmount)
    {
        serveAmount = taskServeAmount;
        _serveAmountText.text = ((int)serveAmount).ToString();
    }

    //public void RefillDrinkWater()
    //{
    //    if (_drinkWaterServeAmount < _maxDrinkWaterServeAmount)
    //    {  
    //        _drinkWaterServeAmount += 1 * Time.time;
    //        UpdateOnServeAmountUI(_drinkWaterServeAmount);
    //        print("ASD");
    //    }
    //}

    public IEnumerator RefillDrinkWater()
    {
        if (_drinkWaterServeAmount < _maxDrinkWaterServeAmount)
        {
            _drinkWaterServeAmount += 1 * Time.deltaTime;
            UpdateOnServeAmountUI(_drinkWaterServeAmount);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void OnDrinkWaterTask()
    {
        _taskIcon.sprite = _taskIcons[0];
        UpdateOnServeAmountUI(_drinkWaterServeAmount);
        StartCoroutine(RefillDrinkWater());
    }

    public void OnUmbrellaTask()
    {
        _taskIcon.sprite = _taskIcons[1];
        UpdateOnServeAmountUI(_umbrellaServeAmount);
        TakeUmbrella(_umbrellaTask);
    }
}
