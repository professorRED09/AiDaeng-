using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    //[SerializeField] float timeUsed;

    [Header("Ref")]
    public Transform waypoint;              // The waypoint for this task
    public int index;                       // Index of this waypoint    
    private PlayerController1 player;       // Reference for player script
    public Items goalItem;

    public bool isFirstTaskOfTheProduct;
    private AudioSource audioSource;
    //[SerializeField] AudioClip soundFX;

    [Header("If the task requires an item...")]
    [SerializeField] bool isRequiredItem;
    [SerializeField] Items requiredItem;
    

    Animator animator;
    private MixturePanel mixturePanel;

    private void Start()
    {        
        player = FindObjectOfType<PlayerController1>(); // get reference from player script
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        mixturePanel = FindObjectOfType<MixturePanel>();

        //if (soundFX == null) return;
        //audioSource.clip = soundFX;
    }

    IEnumerator DoTask()
    {
        //if (isFirstTaskOfTheProduct)
        //{
        //    mixturePanel.OpenPanel();
        //}
        //audioSource.Play(); // play soundFX
        yield return new WaitForSeconds(0.7f);
        // Add code to pop up a mixture window, maybe check if the mixture is showing already or not
        // if mixture window haven't been poped up yet; show it
        
        if (isRequiredItem)
        {
            if (Inventory.instance.HasItem(requiredItem))
            {
                Inventory.instance.UpdateItem(requiredItem, goalItem);  
            }
            else
            {
                NotificationManager.instance.AddNoti($"ต้องมี{requiredItem.name}ก่อนจึงสามารถทำได้!");
                //if (mixturePanel.isOpened)
                //{
                //    mixturePanel.ClosePanel();
                //}
            }
        }
        else
        {  
            Inventory.instance.AddItem(goalItem);
        }
       
    }
    
    // To check if player have arrived at the task
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && player.targetWP == gameObject)
        {
            StartCoroutine(DoTask());            
        }
        else
        {
            //print("ASDAEWAEWEQW");
        }
    }    

    private void OnMouseDown()
    {
        audioSource.Play(); // play soundFX
        // when players click on this task, the character will move to this task  
        if (waypoint != null && player != null)
        {
            player.MoveToWaypoint(waypoint);
            player.targetWP = gameObject;
            //Debug.Log("MOVING TO TASK: " + gameObject.name);  
        }
    }

    void OnMouseOver()
    {
        animator.SetBool("isHovered", true);
    }

    void OnMouseExit()
    {
        animator.SetBool("isHovered", false);
    }




}
