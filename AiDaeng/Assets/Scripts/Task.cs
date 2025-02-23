using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [Header("Ref")]
    public Transform waypoint;  // The waypoint for this task
    public int index;           // Index of this waypoint    
    private PlayerController1 player;    // Reference for player script
    public Items goalItem;

    [Header("If the task requires an item...")]
    [SerializeField] bool isRequiredItem;
    [SerializeField] Items requiredItem;    

    private void Start()
    {        
        player = FindObjectOfType<PlayerController1>(); // get reference from player script
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && player.targetWP == gameObject)
        {
            //Debug.Log("PLAYER ARRIVES AT " + gameObject.name);
            if (isRequiredItem)
            {
                if (Inventory.instance.HasItem(requiredItem))
                {
                    print("GET " + goalItem.name);
                    Inventory.instance.UpdateItem(requiredItem,goalItem);
                }
                else
                {
                    print("GO GET " + requiredItem.name + " FIRST!");
                }
            }
            else
            {
                print("GET " + goalItem.name);
                Inventory.instance.AddItem(goalItem);
            }
        }
        else
        {
            print("SDFF");
        }
    }

    private void OnMouseDown()
    {
        // when players click on this task, the character will move to this task  
        if (waypoint != null && player != null)
        {
            player.MoveToWaypoint(waypoint);
            player.targetWP = gameObject;
            Debug.Log("MOVING TO TASK: " + gameObject.name);
            
            //if (isRequiredItem)
            //{
            //    if (Inventory.instance.HasItem(requiredItem))
            //    {
            //        print("Get " + goalItem.name);
            //        Inventory.instance.AddItem(goalItem);
            //    }
            //    else
            //    {
            //        print("Go get " + requiredItem.name + " first!");
            //    }
            //}
            //else
            //{
            //    print("Do task");
            //    Inventory.instance.AddItem(goalItem);
            //}
        }

        //void DoTask()
        //{

        //}
    }

    


}
