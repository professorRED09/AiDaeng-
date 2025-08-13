using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkWaterBuff : MonoBehaviour
{
    //[SerializeField]
    private Image _buffImageFill;

    [SerializeField]
    private bool _isPermaBuff;

    [SerializeField]
    private float _effectTime;

    private float _currentTime;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _effectTime;
        _buffImageFill = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPermaBuff)
        {
            _currentTime -= Time.deltaTime;
            float targetFillAmount = _currentTime / _effectTime;            
            _buffImageFill.fillAmount = targetFillAmount;

            if(_currentTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
