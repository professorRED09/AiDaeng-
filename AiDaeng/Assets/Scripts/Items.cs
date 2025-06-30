using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item", fileName = "New item")]

public class Items : ScriptableObject
{
    public string itemName;    
    public Sprite itemSprite;
    public bool isCompleted;
}
