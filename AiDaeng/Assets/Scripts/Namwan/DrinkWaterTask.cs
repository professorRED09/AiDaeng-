using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWaterTask : MonoBehaviour
{
    [Header("Ref")]
    public Transform waypoint;              // The waypoint for this task
    /*public int index;  */                     // Index of this waypoint    
    private Namwan namwan;       // Reference for player script
    //public Items goalItem;
    private AudioSource audioSource;
    //[SerializeField] AudioClip soundFX;

    private void Start()
    {
        namwan = FindObjectOfType<Namwan>(); // get reference from player script
        audioSource = GetComponent<AudioSource>();
    } 

    private void OnMouseDown()
    {
        // when players click on this task, the character will move to this task  
        if (waypoint != null && namwan != null)
        {
            namwan.MoveToWaypoint(waypoint);

            namwan.currentTask = Namwan.CurrentTask.DrinkWater;
            namwan.ChangeTask();
        }
    }
}
