using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waypoint", fileName = "New waypoint")]

public class Waypoints : ScriptableObject
{
    public string waypointName;
    public Vector3 transform;
}
