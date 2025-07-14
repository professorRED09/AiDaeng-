using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Namwan : MonoBehaviour
{
    [Header("Ref")]
    public List<Transform> waypoints;   // [0] is the drinking water waypoint
    public Transform currentWaypoint;
    public Queue<Transform> pathQueue = new Queue<Transform>();   // Waypoints to visit
    private Vector3 direction;
    public TMP_Text serveAmountText;
    public NPC2 targetNPC;
    [SerializeField] NamwanInventory _inventory;
    
    [Header("Setting")]
    public float moveSpeed = 5f;    // Movement speed
    
    
    private bool _isMoving = false;
    public bool canServe = true;
    [SerializeField] float _maxServeAmount = 4f; // Max amount of water cups that Namwan can carries
    private float serveAmount;

    public enum CurrentTask
    {
        DrinkWater, Umbrella
    }

    public CurrentTask currentTask;

    // Start is called before the first frame update
    void Start()
    {
        //set initial serve amount
        serveAmount = _maxServeAmount;
        serveAmountText.text = serveAmount.ToString();

        //set initial task
        currentTask = CurrentTask.DrinkWater;
        ChangeTask();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving && pathQueue.Count > 0)
        {
            MoveToNextWaypoint();
        }
        else
        {
            _isMoving = false;
        }

        // check if Namwan is at a drink water waypoint, if yes, then fill water
        if (transform.position == waypoints[0].position)
        {                      
            RefillDrinkWater();
        }
        

        if (serveAmount < 1)
        {
            canServe = false;
        }
        else
        {
            canServe = true;
        }
    }

    public void ChangeTask()
    {
        switch (currentTask)
        {
            case CurrentTask.DrinkWater:
                _inventory.OnDrinkWaterTask();
                break;
            case CurrentTask.Umbrella:
                _inventory.OnUmbrellaTask();
                break;
        }
    }
    

    void RefillDrinkWater()
    {        
        if (serveAmount < _maxServeAmount)
        {
            //audioSource.Play(); // play soundFX
            serveAmount += 1 * Time.deltaTime;
            serveAmountText.text = ((int)serveAmount).ToString();
        }        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.CompareTag("DrinkWater"))
    //    {
    //        StartCoroutine(RefillDrinkWater());
    //    }
    //}

    IEnumerator GiveDrinkWater()
    {
        _isMoving = false;
        if (serveAmount >= 1 /*&& targetNPC != null*/)
        {
            targetNPC.GetComponent<NPC2>().ResetMoodBar();      // reset npc mood
            serveAmount -= 1;                                   // deduct drink water from Namwan
            serveAmountText.text = ((int)serveAmount).ToString();         // update on UI
            targetNPC = null;                                   // set target to null
            Invoke(nameof(BackToRefill), 4f);
        }
        else
        {
            //print("YOU DON'T HAVE ANY DRINKS LEFT!");
        }
        //yield return new WaitForSeconds(1f);
        yield return null;
    }

    void BackToRefill()
    {
        if (_isMoving) return;
        MoveToWaypoint(waypoints[0]);        
    }

    public void MoveToWaypoint(Transform targetWaypoint)
    {
        if (_isMoving || targetWaypoint == null)
            return;
        // Find the nearest waypoint
        Transform nearestWaypoint = GetNearestWaypoint(targetWaypoint); 

        if (nearestWaypoint != null)
        {
            // Generate the path (nearest waypoint -> intermediate waypoints -> target)
            List<Transform> path = GetPath(currentWaypoint, nearestWaypoint);
            pathQueue = new Queue<Transform>(path);
            _isMoving = true;            
        }
    }

    // Move to the next waypoint in the queue
    private void MoveToNextWaypoint()
    {
        Transform nextWaypoint = pathQueue.Peek();
        direction = (nextWaypoint.position - transform.position).normalized;

        transform.position += moveSpeed * Time.deltaTime * direction;

        // Check if player reached the waypoint, then continue to the next one
        if (Vector3.Distance(transform.position, nextWaypoint.position) < 0.1f)
        {
            transform.position = nextWaypoint.position; // Snap to waypoint
            currentWaypoint = nextWaypoint;
            pathQueue.Dequeue(); // Remove the visited waypoint

            if (pathQueue.Count <= 0 && targetNPC != null)
            {
                StartCoroutine(GiveDrinkWater());
            }
        }
        
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
