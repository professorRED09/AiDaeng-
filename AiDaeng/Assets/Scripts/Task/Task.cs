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

        if (isFirstTaskOfTheProduct || mixturePanel.tempItem == requiredItem)
        {
            mixturePanel.ShowPanel();

            // player can't move during task
            player.canControl = false;

            player.GetComponent<Animator>().SetBool("isDoingTask", true);
            audioSource.clip = taskSFX;
            audioSource.Play(); // play soundFX
            mixturePanel.UpdateTempItem(goalItem);
            yield return new WaitForSeconds(0.7f);
            player.GetComponent<Animator>().SetBool("isDoingTask", false);
            // update on UI to show which process player is currently at            

            // player can move again
            player.canControl = true;
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
            // if player target waypoint is this task and they're already here, start doing task
            if (player.targetWP == gameObject && playerIsAtTask)
            {
                StartCoroutine(DoTask());
            }
            else if (player.canControl) {
                player.MoveToWaypoint(waypoint);
                player.targetWP = gameObject;
                //Debug.Log("MOVING TO TASK: " + gameObject.name);  
            }
        }
        else
        {
            Debug.LogWarning("You haven't assign waypoint for this task or assign player yet");
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
