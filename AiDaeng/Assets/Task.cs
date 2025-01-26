using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public Transform waypoint;  // The waypoint for this task
    public int index;           // Index of this waypoint    
    private PlayerController player;    // Reference for player script

    private void Start()
    {        
        player = FindObjectOfType<PlayerController>(); // get reference from player script
    }

    private void OnMouseDown()
    {
        // when players click on this task, the character will move to this task  
        if (waypoint != null && player != null)
        {
            player.MoveToWaypoint(index);
            Debug.Log("Moving to task: " + gameObject.name);            
        }
    }


}
