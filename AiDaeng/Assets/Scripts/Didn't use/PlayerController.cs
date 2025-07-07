//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class PlayerController : MonoBehaviour
//{
//    [Header("Setting")]
//    public float speed = 5f;                                // Movement speed
//    [SerializeField] int currentWaypointIndex = 0;          // Current waypoint index
//    public bool reachWayPoint;
//    [SerializeField] bool isMoving = false;                 // Is the player currently moving?
//    public List<Transform> waypoints;                       // List of all waypoints in order
//    private Queue<int> waypointQueue = new Queue<int>();    // Queue to store the waypoints to visit
    

//    void Update()
//    {
//        // if players click on the task, move player to the waypoint of that task
//        if (isMoving && waypointQueue.Count > 0) 
//        {
//            MoveToNextWaypoint();
//        }
//    }

//    // Function to start moving to a specific target waypoint
//    public void MoveToWaypoint(int targetWaypointIndex)
//    {
//        if (targetWaypointIndex < 0 || targetWaypointIndex >= waypoints.Count)
//        {
//            Debug.LogError("Invalid waypoint index!");
//            return;
//        }

//        // Clear any previous movement and calculate the path
//        waypointQueue.Clear();

//        // Add waypoints from the current index to the target index
//        if (currentWaypointIndex < targetWaypointIndex)
//        {
//            for (int i = currentWaypointIndex + 1; i <= targetWaypointIndex; i++)
//            {
//                waypointQueue.Enqueue(i);
//            }
//        }
//        else if (currentWaypointIndex > targetWaypointIndex)
//        {
//            for (int i = currentWaypointIndex - 1; i >= targetWaypointIndex; i--)
//            {
//                waypointQueue.Enqueue(i);
//            }
//        }

//        isMoving = true;
//    }

//    // Function to move the player to the next waypoint in the queue
//    private void MoveToNextWaypoint()
//    {
//        if (waypointQueue.Count == 0)
//        {
//            isMoving = false;
//            return;
//        }

//        int nextWaypointIndex = waypointQueue.Peek();
//        Transform targetWaypoint = waypoints[nextWaypointIndex];

//        // Move the player toward the target waypoint
//        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
//        transform.position += direction * speed * Time.deltaTime;

//        // Check if the player has reached the waypoint
//        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
//        {
//            transform.position = targetWaypoint.position; // Snap to the waypoint
//            currentWaypointIndex = waypointQueue.Dequeue(); // Update current index
//            reachWayPoint = true;
//        }
//    }


//}
