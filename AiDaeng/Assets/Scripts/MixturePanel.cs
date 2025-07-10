using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixturePanel : MonoBehaviour
{
    [Header("Setting")]
    public Items tempItem;
    private Image tempItemImage;
    private AudioSource _audioSource;

    private bool isOpened;
    private Animator animator;    

    private void Awake()
    {        
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
            Inventory.instance.AddItem(item);
            //enable "เสร็จแล้วจ้า!" text to inform player            
            transform.GetChild(1).gameObject.SetActive(true);
            tempItem = null;
            _audioSource.Play();
            //if player don't do any task in time, hide it
            StartCoroutine(OnCompleteTask());
            
        }
    }

    IEnumerator OnCompleteTask()
    {
        yield return new WaitForSeconds(1f);
        if(tempItem == null)
        {
            ClosePanel();
        }
    }

    // gradually disappear when item is complete
    


}
