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

    public Queue<Transform> taskQueue = new Queue<Transform>();
    [SerializeField] private bool isMoving = false;

    public GameObject targetWP;
    Animator animator;

    public Transform counterWP;
    public bool isAtCounter;           // check if player is gonna give product to npc
    public bool isFacingRight;      // our based sprite for walking is facing in the right direction
    //public bool isAtPosition;
    public bool canControl = false; // player won't be able to control until the game start

    public Vector3 direction;  // set as public for debugging
    public NPC2 targetNPC;


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

        // if there are path left for player, keep moving
        if (isMoving && pathQueue.Count > 0)
        {
            MoveToNextWaypoint();
        }
        else
        {
            isMoving = false;
        }

        // check if player's at the counter
        if (transform.position == counterWP.position)
        {
            isAtCounter = true;
        }
        else
        {
            isAtCounter = false;
        }

        if (isAtCounter && targetNPC != null)
        {
            targetNPC.GetComponent<NPC2>().CheckProductFromPlayer();
            targetNPC = null;
        }

    }

    public void SetTargetNPC(NPC2 npc)
    {
        targetNPC = npc;
    }

    public void AddTask(Transform task)
    {
        //add task to the list and start move to that waypoint        
        taskQueue.Enqueue(task);
        //check if player is free       
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
            MoveToNextWaypoint();
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
