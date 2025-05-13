using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Namwan : MonoBehaviour
{
    [Header("Ref")]
    /*public List<Transform> waypoints;  */                             // All waypoints in the scene
    public Transform[] waypoints;
    public Queue<Transform> pathQueue = new Queue<Transform>();     // Waypoints to visit

    [Header("Setting")]
    public float speed = 5f;                                        // Movement speed

    public List<Transform> taskQueue;
    [SerializeField] private bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        print(float.MaxValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    private void OnMouseEnter()
    {        
        print("NAMWAN");
    }

    // Find the nearest waypoint to the selected npc's current position
    private Transform GetNearestWaypoint(Transform npcTransform)
    {
        Transform nearest = null;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector2.Distance(npcTransform.position, waypoints[i].position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = waypoints[i];
            }
        }
        return nearest;

        //Transform nearest = null;
        //float shortestDistance = float.MaxValue;

        //foreach (Transform waypoint in waypoints)
        //{
        //    float distance = Vector3.Distance(transform.position, waypoint.position);
        //    if (distance < shortestDistance)
        //    {
        //        shortestDistance = distance;
        //        nearest = waypoint;
        //    }
        //}

        //return nearest;
    }

}
