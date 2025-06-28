using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixturePanel : MonoBehaviour
{
    [Header("Setting")]
    public Items tempItem;
    private Image tempItemImage;

    private bool isOpened;
    private Animator animator;    

    private void Awake()
    {        
        animator = GetComponent<Animator>();
        tempItemImage = transform.GetChild(0).GetComponent<Image>();        
    }   

    public void OpenPanel()
    {
        isOpened = true;
        animator.SetBool("isOpened", isOpened);
    }

    public void ClosePanel()
    {
        isOpened = false;
        animator.SetBool("isOpened", isOpened);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void UpdateTempItem(Items item)
    {        
        tempItem = item;
        tempItemImage.sprite = item.itemSprite;
        if (item.isCompleted)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            tempItem = null;
            Invoke(nameof(ClosePanel), 1f);
        }
    }

    // gradually disappear when item is complete
    


}
