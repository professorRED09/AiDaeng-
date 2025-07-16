using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item", fileName = "New item")]

public class Items : ScriptableObject
{
    public enum ItemType
    {
        ไอเทมผสม, ไอเทมเดี่ยว
    }

    public string itemName;
    public ItemType itemType;
    public Sprite itemSprite;
    public bool isCompleted;

    
}
