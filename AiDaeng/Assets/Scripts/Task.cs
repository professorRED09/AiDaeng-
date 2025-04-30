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

    private AudioSource audioSource;
    //[SerializeField] AudioClip soundFX;

    [Header("If the task requires an item...")]
    [SerializeField] bool isRequiredItem;
    [SerializeField] Items requiredItem;    

    private void Start()
    {        
        player = FindObjectOfType<PlayerController1>(); // get reference from player script
        audioSource = GetComponent<AudioSource>();

        //if (soundFX == null) return;
        //audioSource.clip = soundFX;
    }

    IEnumerator DoTask()
    {
        audioSource.Play(); // play soundFX
        yield return new WaitForSeconds(0.7f);
        if (isRequiredItem)
        {
            if (Inventory.instance.HasItem(requiredItem))
            {
                //StartCoroutine(DoTask());
                //print("GET " + goalItem.name);
                Inventory.instance.UpdateItem(requiredItem, goalItem);
                //if (audioSource.clip != null)
                //{
                //    audioSource.Play();
                //}                 
            }
            else
            {
                print("GO GET " + requiredItem.name + " FIRST!");
            }
        }
        else
        {
            //print("GET " + goalItem.name);
            //StartCoroutine(DoTask());
            Inventory.instance.AddItem(goalItem);

            //if (audioSource.clip != null)
            //{
            //    audioSource.Play();
            //}
        }
    }
    
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
        // when players click on this task, the character will move to this task  
        if (waypoint != null && player != null)
        {
            player.MoveToWaypoint(waypoint);
            player.targetWP = gameObject;
            //Debug.Log("MOVING TO TASK: " + gameObject.name);  
        }
    }

    


}
