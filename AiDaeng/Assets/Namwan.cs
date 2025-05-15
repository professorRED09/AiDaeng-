using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Namwan : MonoBehaviour
{
    [Header("Ref")]
    public List<Transform> waypoints;
    public Transform currentWaypoint;
    public Queue<Transform> pathQueue = new Queue<Transform>();   // Waypoints to visit
    public Vector3 direction;
    public TMP_Text serveAmountText;
    public NPC2 targetNPC;

    [Header("Setting")]
    public float moveSpeed = 5f;    // Movement speed
    public float fillSpeed;    
    
    [SerializeField] private bool isMoving = false;

    public float maxServeAmount; // Max amount of water cups that Namwan can carries
    public float serveAmount;

    // Start is called before the first frame update
    void Start()
    {
        serveAmount = maxServeAmount;
        serveAmountText.text = serveAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && pathQueue.Count > 0)
        {
            MoveToNextWaypoint();            
        }
        else if (pathQueue.Count <= 0)
        {
            isMoving = false;
            StartCoroutine(GiveDrinkWater());
        }

        // check if Namwan is at a drink water waypoint, if yes, then fill water
        if (transform.position == waypoints[0].position) {
            if (serveAmount >= maxServeAmount) return;
            else
            {
                StartCoroutine(FillDrinkWater());
            }
        }    
        
    }

    IEnumerator GiveDrinkWater()
    {
        if (targetNPC != null && serveAmount > 0)
        {
            targetNPC.GetComponent<NPC2>().ResetMoodBar();      // reset npc mood
            serveAmount -= 1;                                   // deduct drink water from Namwan
            serveAmountText.text = serveAmount.ToString();      // update on UI
            targetNPC = null;                                   // set target to null
            
        }        
        yield return null;
    }

    IEnumerator FillDrinkWater()
    {
        //audioSource.Play(); // play soundFX
        yield return new WaitForSeconds(fillSpeed);
        serveAmount += 1;
        serveAmountText.text = serveAmount.ToString();
    }

    private void OnMouseEnter()
    {        
        print("MOUSE OVER NAMWAN");
    }

    public void MoveToWaypoint(Transform targetWaypoint)
    {
        if (isMoving || targetWaypoint == null)
            return;

        // Find the nearest waypoint
        Transform nearestWaypoint = GetNearestWaypoint(targetWaypoint);
        print(nearestWaypoint.gameObject.name);

        if (nearestWaypoint != null)
        {
            // Generate the path (nearest waypoint -> intermediate waypoints -> target)
            List<Transform> path = GetPath(currentWaypoint, nearestWaypoint);
            pathQueue = new Queue<Transform>(path);
            isMoving = true;
        }
    }

    // Move to the next waypoint in the queue
    private void MoveToNextWaypoint()
    {
        Transform nextWaypoint = pathQueue.Peek();
        direction = (nextWaypoint.position - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        // Check if player reached the waypoint, then continue to the next one
        if (Vector3.Distance(transform.position, nextWaypoint.position) < 0.1f)
        {
            transform.position = nextWaypoint.position; // Snap to waypoint
            currentWaypoint = nextWaypoint;
            pathQueue.Dequeue(); // Remove the visited waypoint            
        }
        //// check if player reached the final waypoint 
        //if (Vector3.Distance(transform.position, pathQueue[]) < 0.1f)
        //{
        //    transform.position = nextWaypoint.position; // Snap to waypoint
        //}
    }

    // Generate the path from startWaypoint to targetWaypoint 
    private List<Transform> GetPath(Transform startWaypoint, Transform targetWaypoint)
    {
        List<Transform> path = new List<Transform>();

        // Assume waypoints are in order
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

    // Find the nearest waypoint to the selected npc's current position
    private Transform GetNearestWaypoint(Transform npcTransform)
    {
        Transform nearest = null;
        float shortestDistance = float.MaxValue;
        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector2.Distance(npcTransform.position, waypoint.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = waypoint;
            }
        }
        return nearest;
    }
}
