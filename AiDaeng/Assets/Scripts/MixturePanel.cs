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
    [SerializeField] private bool _isClicked = false;
    private Animator animator;
    private float _defaultTimer = 2f;
    private float _timer;
    private bool _isUIVisible = false;

    private void Awake()
    {        
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        tempItemImage = transform.GetChild(0).GetComponent<Image>();

        _timer = _defaultTimer;
    }

    private void Update()
    {
        // Detect any input
        if (_isClicked)
        {
            // Reset timer and show UI if it's hidden
            _timer = _defaultTimer;
            _isClicked = false;

            if (!_isUIVisible)
            {
                ShowPanel();
            }
        }
        else
        {
            // Count time since last input
            _timer -= Time.deltaTime;

            if ( _timer <= 0 && _isUIVisible && tempItem == null)
            {
                HidePanel();
            }
        }
    }

    public void ShowPanel()
    {
        //isOpened = true;
        _isClicked = true;
        //animator.SetBool("isOpened", isOpened);
        _isUIVisible = true;
        animator.SetTrigger("Open");
    }

    public void HidePanel()
    {
        if (_isClicked) return;
        //isOpened = false;
        //animator.SetBool("isOpened", isOpened);
        _isUIVisible = false;
        animator.SetTrigger("Close");        
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
            tempItemImage.sprite = item.itemSprite;

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
            tempItemImage.sprite = null;

            //disable "เสร็จแล้วจ้า!" text   
            transform.GetChild(1).gameObject.SetActive(false);
            //_isClicked = false;
            //HidePanel();
        }
    }

    
    


}
