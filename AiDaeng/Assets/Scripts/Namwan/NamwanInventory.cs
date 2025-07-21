using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NamwanInventory : MonoBehaviour
{   
    [SerializeField] private Sprite[] _taskIcons;
    [SerializeField] private Image _taskIcon;

    [SerializeField] private TMP_Text _serveAmountText;    
    public float serveAmount;

    [Header("Settings")]
    private float _drinkWaterServeAmount;
    [SerializeField] float _maxDrinkWaterServeAmount; // Max amount of water cups that Namwan can carries
    private float _umbrellaServeAmount;
    [SerializeField] float _maxUmbrellaServeAmount; // Max amount of water cups that Namwan can carries

    public bool canServe = true;

    // Start is called before the first frame update
    void Start()
    {
        _drinkWaterServeAmount = _maxDrinkWaterServeAmount;
        _umbrellaServeAmount = _maxUmbrellaServeAmount;

        //initial task will be drink water task
        serveAmount = _drinkWaterServeAmount;
        _serveAmountText.text = serveAmount.ToString();
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

    public void DeductDrinkWater()
    {
        serveAmount -= 1;
        _serveAmountText.text = ((int)serveAmount).ToString();
    }

    public void RefillDrinkWater()
    {
        if (serveAmount < _maxDrinkWaterServeAmount)
        {
            //audioSource.Play(); // play soundFX
            serveAmount += 1 * Time.deltaTime;
            _serveAmountText.text = ((int)serveAmount).ToString();
        }
    }
    public void OnDrinkWaterTask()
    {
        _taskIcon.sprite = _taskIcons[0];
        serveAmount = _drinkWaterServeAmount;
        _serveAmountText.text = serveAmount.ToString();
    }

    public void OnUmbrellaTask()
    {
        _taskIcon.sprite = _taskIcons[1];
        serveAmount = _umbrellaServeAmount;
        _serveAmountText.text = serveAmount.ToString();
    }
}
