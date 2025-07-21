using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [Header("Setting")]
    public AudioClip taskSFX;
    public AudioClip clickSFX;
   
    public Transform waypoint;              // The waypoint for this task     
    private PlayerController1 player;       // Reference for player script
    public Items goalItem;

    public bool isFirstTaskOfTheProduct;
    public bool playerIsAtTask;
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
    }

    IEnumerator DoTask()
    {        
        if (isFirstTaskOfTheProduct)
        {            
            mixturePanel.OpenPanel();

            player.GetComponent<Animator>().SetBool("isDoingTask", true);
            audioSource.clip = taskSFX;
            audioSource.Play(); // play soundFX
            yield return new WaitForSeconds(0.7f);
            player.GetComponent<Animator>().SetBool("isDoingTask", false);

            // update on UI to show which process player is currently at
            mixturePanel.UpdateTempItem(goalItem);
        }
        else
        {
            //Debug.Log("PLEASE FINISH YOUR CURRENT PRODUCT FIRST");
        }

        //player.GetComponent<Animator>().SetBool("isDoingTask", true);
        //audioSource.clip = taskSFX;
        //audioSource.Play(); // play soundFX
        //yield return new WaitForSeconds(0.7f);
        //player.GetComponent<Animator>().SetBool("isDoingTask", false);

        // Add code to pop up a mixture window, maybe check if the mixture is showing already or not
        // if mixture window haven't been poped up yet; show it

        if (isRequiredItem)
        {
            // if tempItem is correct, update it. if not, delete tempItem
            if(mixturePanel.tempItem == requiredItem)
            {
                player.GetComponent<Animator>().SetBool("isDoingTask", true);
                audioSource.clip = taskSFX;
                audioSource.Play(); // play soundFX
                yield return new WaitForSeconds(0.7f);
                player.GetComponent<Animator>().SetBool("isDoingTask", false);

                mixturePanel.UpdateTempItem(goalItem);                
            }
            //else if (mixturePanel.tempItem.itemType == requiredItem.itemType)
            //{                
            //    print("GO GET THE REQUIRED COMPONENT FIRST!");
            //}
            else {
                print("GO GET THE REQUIRED COMPONENT FIRST!");
            }
        }
        else if (!isFirstTaskOfTheProduct)
        {
            player.GetComponent<Animator>().SetBool("isDoingTask", true);
            audioSource.clip = taskSFX;
            audioSource.Play(); // play soundFX
            yield return new WaitForSeconds(0.7f);
            player.GetComponent<Animator>().SetBool("isDoingTask", false);

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
    private void OnTriggerStay2D(Collider2D col)
    {
        playerIsAtTask = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        playerIsAtTask = false;
    }

    private void OnMouseDown()
    {
        audioSource.clip = clickSFX;
        audioSource.Play(); // play soundFX
        // when players click on this task, the character will move to this task  
        if (waypoint != null && player != null)
        {
            if (player.targetWP == gameObject && playerIsAtTask)
            {
                StartCoroutine(DoTask());
            }
            else {
                player.MoveToWaypoint(waypoint);
                player.targetWP = gameObject;
                //Debug.Log("MOVING TO TASK: " + gameObject.name);  
            }



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
