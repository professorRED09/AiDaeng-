using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [Header("Ref")]
    public List<Transform> waypoints;                               // All waypoints in the scene
    public Queue<Transform> pathQueue = new Queue<Transform>();     // Waypoints to visit
    [Header("Setting")]
    public float speed = 5f;    // Movement speed
    
    public List<Transform> taskQueue;
    [SerializeField] private bool isMoving = false;

    public GameObject targetWP;
    Animator anim;

    public Transform counterWP;
    public bool isAtCounter;

    Vector3 direction;

    private void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {        
        if(pathQueue.Count <= 0)
        {
            isMoving = false;
        }
        
        if (isMoving && pathQueue.Count > 0)
        {
            MoveToNextWaypoint();
        }    
        
        if(transform.position == counterWP.position)
        {
            isAtCounter = true;
        }
        else
        {
            isAtCounter = false;
        }

    }

    // Start moving toward the nearest waypoint first, then to the target waypoint
    public void MoveToWaypoint(Transform targetWaypoint)
    {
        if (isMoving || targetWaypoint == null)
            return;

        // Find the nearest waypoint
        Transform nearestWaypoint = GetNearestWaypoint();

        if (nearestWaypoint != null)
        {
            // Generate the path (nearest waypoint -> intermediate waypoints -> target)
            List<Transform> path = GetPath(nearestWaypoint, targetWaypoint);
            pathQueue = new Queue<Transform>(path);
            isMoving = true;
        }
    }

    // Move to the next waypoint in the queue
    private void MoveToNextWaypoint()
    {
        Transform nextWaypoint = pathQueue.Peek();
        direction = (nextWaypoint.position - transform.position).normalized;
        //if(direction.x > 0)
        //{
        //    anim.S
        //}
        //else
        //{

        //}
        transform.position += direction * speed * Time.deltaTime;

        // Check if player reached the waypoint
        if (Vector3.Distance(transform.position, nextWaypoint.position) < 0.1f)
        {
            transform.position = nextWaypoint.position; // Snap to waypoint
            pathQueue.Dequeue(); // Remove the visited waypoint            
        }        
    }

    // Find the nearest waypoint to the player's current position
    private Transform GetNearestWaypoint()
    {
        Transform nearest = null;
        float shortestDistance = float.MaxValue;

        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(transform.position, waypoint.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = waypoint;
            }
        }

        return nearest;
    }

    // Generate the path from startWaypoint to targetWaypoint (modify for A* if needed)
    private List<Transform> GetPath(Transform startWaypoint, Transform targetWaypoint)
    {
        List<Transform> path = new List<Transform>();

        // Assume waypoints are in order; modify for A* if needed
        int startIndex = waypoints.IndexOf(startWaypoint);
        int targetIndex = waypoints.IndexOf(targetWaypoint);

        if (startIndex <= targetIndex)
        {
            for (int i = startIndex; i <= targetIndex; i++)
                path.Add(waypoints[i]);
        }
        else
        {
            for (int i = startIndex; i >= targetIndex; i--)
                path.Add(waypoints[i]);
        }

        return path;
    }
}
