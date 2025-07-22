using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image _itemImage;
    [SerializeField] GameObject _addItemPrefab;
    [SerializeField] GameObject _removeItemPrefab;    
    [SerializeField] Image _lifeBarFillImage;

    private Sprite _currentSprite;

    private float _maxLife = 10;
    private float _currentLife;

    [SerializeField] private Items currentItem;
    [SerializeField] private bool _isRaining;
    private WeatherManager _weatherManager;

    private void Awake()
    {
        _weatherManager = FindObjectOfType<WeatherManager>();
    }

    private void Start()
    {
        _currentSprite = _itemImage.sprite;

        _currentLife = _maxLife;
    }

    private void Update()
    {
        if (!_isRaining) return;

        _currentLife -= 0.2f * Time.deltaTime;
        UpdateLifeBar();
        
    }

    void UpdateLifeBar()
    {
        float targetFillAmount = _currentLife / _maxLife;
        _lifeBarFillImage.fillAmount = targetFillAmount;
    }

    public void SetItem(Items m_items)
    {
        currentItem = m_items;
        OnWeatherChange();
    }

    void OnWeatherChange()
    {
        if (_weatherManager.currentState == _weatherManager.rainyState && _isRaining == false && currentItem != null)
        {
            _isRaining = true;
        }        
    }

    public void CountdownLifeBar()
    {
        if (currentItem == null) return;
        _isRaining = true;
    }

    public void OnChanged(Sprite newSprite)
    {        
        if (newSprite != _currentSprite)
        {
            if (_currentSprite == null && newSprite != null)
            {
                Vector3 thePosition = transform.TransformPoint(transform.position);
                Instantiate(_addItemPrefab, thePosition, Quaternion.identity);
            }                

            else if (_currentSprite != null && newSprite == null)
            {
                Vector3 thePosition = transform.TransformPoint(transform.position);
                Instantiate(_removeItemPrefab, thePosition, Quaternion.identity);
            }

            else
            {
                Debug.Log("Image sprite has changed to a different sprite.");
            }

            _currentSprite = newSprite;
        }       

    }
}
