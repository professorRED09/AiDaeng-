using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waypoint", fileName = "New waypoint")]

public class Waypoints : ScriptableObject
{
    //public GameObject waypoint;
    public string waypointName;
    public Vector3 transform;

    //private void Awake()
    //{
    //    transform = waypoint.transform.position;
    //}
}
