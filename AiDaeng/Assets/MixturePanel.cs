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
        this.gameObject.SetActive(true);
        animator.SetBool("isOpened", isOpened);
    }

    public void ClosePanel()
    {
        isOpened = false;
        this.gameObject.SetActive(isOpened);
        animator.SetBool("isOpened", isOpened);
    }

    public void UpdateTempItem(Items item)
    {
        tempItem = item;
        tempItemImage.sprite = item.itemSprite;
    }


}
