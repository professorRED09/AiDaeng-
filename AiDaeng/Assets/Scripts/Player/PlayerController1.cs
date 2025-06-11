using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [Header("Ref")]
    public List<Transform> waypoints;                               // All waypoints in the scene
    public Queue<Transform> pathQueue = new Queue<Transform>();     // Waypoints to visit

    [Header("Setting")]
    public float speed = 5f;                                        // Movement speed

    public List<Transform> taskQueue;
    [SerializeField] private bool isMoving = false;

    public GameObject targetWP;
    Animator animator;

    public Transform counterWP;
    public bool isAtCounter;
    public bool isFacingRight;      // our based sprite for walking is facing in the right direction
    public bool isAtPosition;
    public bool canControl = false; // player won't be able to control until the game start

    public Vector3 direction;  // set as public for debugging


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Animate();
        
        if(direction.x > 0 && !isFacingRight || direction.x < 0 && isFacingRight)
        {
            Flip();
        }

        if (pathQueue.Count <= 0)
        {
            isMoving = false;
            isAtPosition = true;

        }

        if (isMoving && pathQueue.Count > 0)
        {
            MoveToNextWaypoint();
            isAtPosition = false;
        }

        if (transform.position == counterWP.position)
        {
            isAtCounter = true;
        }
        else
        {
            isAtCounter = false;
        }

    }

    public void AddTask(Transform task)
    {
        taskQueue.Add(task);
    }

    // Start moving toward the nearest waypoint first, then to the target waypoint
    public void MoveToWaypoint(Transform targetWaypoint)
    {
        if (isMoving || targetWaypoint == null || !canControl)
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
     
        transform.position += speed * Time.deltaTime * direction;

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

    void Animate()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    void Flip()
    {
        if (!isMoving)
            return;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isFacingRight = !isFacingRight;
    }
}
