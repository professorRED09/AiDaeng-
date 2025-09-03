using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Namwan : MonoBehaviour, IObserver
{
    [Header("Ref")]
    public List<Transform> waypoints;   // [0] is the drinking water waypoint
    public Transform currentWaypoint;
    public Queue<Transform> pathQueue = new Queue<Transform>();   // Waypoints to visit
    private Vector3 direction;

    //public TMP_Text serveAmountText;
    public NPC2 targetNPC;
    public NamwanInventory inventory;
    
    [Header("Setting")]
    public float moveSpeed = 5f;    // Movement speed    
    private bool _isMoving = false;
    private Animator _animator;
    //public bool canServe = true;
    private float _defaultTimer = 6f;
    private float _timer;
    private bool _isCommandedByPlayer;

    //[SerializeField] float _maxServeAmount = 4f; // Max amount of water cups that Namwan can carries
    //private float serveAmount;
    private WeatherManager _weatherManager;
    private AudioSource _audioSource;

    public enum CurrentTask
    {
        DrinkWater, Umbrella
    }

    public CurrentTask currentTask;

    // Start is called before the first frame update

    void Awake()
    {
        _weatherManager = FindObjectOfType<WeatherManager>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        //set initial task
        ChangeTask(CurrentTask.DrinkWater);     
    }

    // Update is called once per frame
    void Update()
    {
        Animate();

        if (_isMoving && pathQueue.Count > 0)
        {
            MoveToNextWaypoint();
        }
        else
        {
            _isMoving = false;
        }

        // check if Namwan is at a drink water waypoint and current task is drinkWater, if yes, then fill water
        if (transform.position == waypoints[0].position && currentTask == CurrentTask.DrinkWater)
        {
            inventory.OnDrinkWaterTask();
        }
        else if (transform.position == waypoints[1].position && currentTask == CurrentTask.Umbrella)
        {
            inventory.OnUmbrellaTask();
        }

        if (_isCommandedByPlayer)
        {
            _timer = _defaultTimer;
            _isCommandedByPlayer = false;
        }
        else
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                BackToTheTask();
            }
        }
    }

    void Animate()
    {
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetFloat("moveX", direction.x);
        _animator.SetFloat("moveY", direction.y);

        _animator.SetFloat("serveAmount", inventory.serveAmount);
    }

    public void ChangeTask(CurrentTask task)
    {
        switch (task)
        {
            case CurrentTask.DrinkWater:
                currentTask = CurrentTask.DrinkWater;
                //inventory.OnDrinkWaterTask();
                break;
            case CurrentTask.Umbrella:
                currentTask = CurrentTask.Umbrella;
                //inventory.OnUmbrellaTask();
                break;
        }
    }

    IEnumerator GiveDrinkWater()
    {
        _isMoving = false;
        if (inventory.serveAmount >= 1 /*&& targetNPC != null*/)
        {
            //targetNPC.GetComponent<NPC2>().ResetMoodBar();
            targetNPC.GetComponent<NPC2>().OnGettingWater();
            inventory.DeductDrinkWater();
            targetNPC = null;                                  
            _audioSource.Play();
            //Invoke(nameof(BackToTheTask), 4f);
        }
        else
        {
            //print("YOU DON'T HAVE ANY DRINKS LEFT!");
        }
        //yield return new WaitForSeconds(1f);
        yield return null;
    }

    IEnumerator GiveUmbrella()
    {        
        _isMoving = false;
        if (inventory.serveAmount >= 1)
        {
            if (!targetNPC.GetComponent<NPC2>().hasUmbrella) {

                targetNPC.GetComponent<NPC2>().OnGettingUmbrella();
                inventory.DecreaseUmbrella();

                targetNPC = null;                                               
                _audioSource.Play();
                //Invoke(nameof(BackToTheTask), 4f);
            }
            else
            {
                Debug.Log("This guy has an umbrella already");                
            }            
        }
        else
        {
            print("YOU DON'T HAVE ANY UMBRELLA LEFT!");
        }
        //yield return new WaitForSeconds(1f);
        yield return null;
    }

    void BackToTheTask()
    {
        if (_isCommandedByPlayer)
        {
            return;
        }

        

        if (currentTask == CurrentTask.DrinkWater)
        {
            MoveToWaypoint(waypoints[0]);
        }
        else
        {
            MoveToWaypoint(waypoints[1]);
        }
    }

    //void BackToRefill()
    //{
    //    if (_isMoving) return;
    //    MoveToWaypoint(waypoints[0]);        
    //}

    public void OnNotify(WeatherType weather)
    {
        switch (weather)
        {
            case (WeatherType.Rainy):
                ChangeTask(CurrentTask.Umbrella);
                return;

            default:
                ChangeTask(CurrentTask.DrinkWater);
                return;
        }
    }

    void OnEnable()
    {
        _weatherManager.AddObserver(this);
    }

    void OnDisable()
    {
        _weatherManager.RemoveObserver(this);
    }

    public void MoveToWaypoint(Transform targetWaypoint)
    {
        if (_isMoving || targetWaypoint == null)
            return;

        _isCommandedByPlayer = true;

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

            if (pathQueue.Count <= 0 && targetNPC != null && transform.position != waypoints[0].position && transform.position != waypoints[1].position) 
            {
                if(currentTask == CurrentTask.DrinkWater)
                {
                    StartCoroutine(GiveDrinkWater());
                }
                else if (currentTask == CurrentTask.Umbrella)
                {
                    StartCoroutine(GiveUmbrella());
                }

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
