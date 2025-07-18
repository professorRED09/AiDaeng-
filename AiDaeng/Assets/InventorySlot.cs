using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image _itemImage;
    [SerializeField] GameObject _addItemPrefab;
    [SerializeField] GameObject _removeItemPrefab;
    Sprite currentSprite;

    private void Start()
    {
        currentSprite = _itemImage.sprite;
    }

    public void OnChanged(Sprite newSprite)
    {        
        if (newSprite != currentSprite)
        {
            if (currentSprite == null && newSprite != null)
            {
                Vector3 thePosition = transform.TransformPoint(transform.position);
                Instantiate(_addItemPrefab, thePosition, Quaternion.identity);
            }                

            else if (currentSprite != null && newSprite == null)
            {
                Vector3 thePosition = transform.TransformPoint(transform.position);
                Instantiate(_removeItemPrefab, thePosition, Quaternion.identity);
            }

            else
            {
                Debug.Log("Image sprite has changed to a different sprite.");
            }

            currentSprite = newSprite;
        }
        

    }
}
