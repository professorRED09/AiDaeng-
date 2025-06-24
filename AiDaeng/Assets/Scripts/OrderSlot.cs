using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSlot : MonoBehaviour
{    
    public Items orderItem;
    public Image orderItemImg;

    public void SetOrderItem(Items item)
    {
        orderItem = item;
        orderItemImg.sprite = item.itemSprite;
    }


}
