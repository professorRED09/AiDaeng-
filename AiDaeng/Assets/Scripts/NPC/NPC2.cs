using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC2 : MonoBehaviour, IObserver
{
    [Header("Setting")]
    [SerializeField] float maxEB; // max value of emotion value
    [SerializeField] float decSpeed; // decreasing speed of emotion value
    float currentDecSpeed;
    public float prayingTime = 2;

    public float speed = 2f;
    private float currentEB; // max value of emotion value
    [SerializeField] Vector3 transOffset;
    [SerializeField] float detectLength;
    [SerializeField] LayerMask npcMask;

    [Header("UI")]
    [SerializeField] GameObject orderBubble;
    [SerializeField] Slider emoBar;
    [SerializeField] Items[] itemLists;

    public Waypoints[] waypoints; // Store assigned waypoints
    public Waypoints[] wayToCounter;
    public Waypoints[] wayToPrey;
    public Waypoints[] wayToLeave;
    public Waypoints[] wayToRageLeave;

    public int currentWaypointIndex = 0;
    [SerializeField] private bool isMoving;

    SpriteRenderer sprite;

    Inventory inventory;
    Subject weatherManager;

    public bool isUpset = false;
    public bool getProduct = false;
    public bool isClickable;
    private bool canMove;
    

    [SerializeField] Items orderItem;

    PlayerController1 player;

    //FSM Stuff
    NPC_BaseState currentState;
    public NPC_ArriveState arriveState = new NPC_ArriveState();
    public NPC_OrderState orderState = new NPC_OrderState();
    public NPC_PreyState preyState = new NPC_PreyState();
    public NPC_LeaveState leaveState = new NPC_LeaveState();
    public NPC_RageLeaveState rageLeaveState = new NPC_RageLeaveState();    

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        inventory = Inventory.instance;
        weatherManager = FindObjectOfType<WeatherManager>();
        player = FindObjectOfType<PlayerController1>();
    }

    void Start()
    {
        // set initial value       
        currentEB = maxEB;
        UpdateEmoBar(currentEB, maxEB);

        // set initial state to begin with
        currentState = arriveState;
        currentState.EnterState(this);
    }

    public void SetWaypoints(Waypoints[] assignedWaypoints)
    {
        waypoints = assignedWaypoints;
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].transform; // Start at the first waypoint
            isMoving = true;
        }
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transOffset, Vector2.right, detectLength, npcMask);

        if (hit.collider == null)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        currentState.UpdateState(this);

        if (isMoving && currentWaypointIndex < waypoints.Length)
        {
            MoveToWaypoint();
        }        
    }

    private void MoveToWaypoint()
    {
        //print("ASDSAS");
        Waypoints targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.transform - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetWaypoint.transform) < 0.1f)
        {
            transform.position = targetWaypoint.transform; // Snap to exact position
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                isMoving = false; // Stop when all waypoints are reached                
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isClickable)
            return;
        player.MoveToWaypoint(player.counterWP);
        if (player.isAtCounter)
        {
            if (Inventory.instance.HasItem(orderItem))      //  check if the player has the product that npc wants or not          
            {
                Inventory.instance.RemoveItem(orderItem);
                getProduct = true;                  // npc gets product
                orderBubble.SetActive(false);       // hide UI
                ScoreManager.instance.AddScore();   // add score for player
                                                    //print("THERE YOU GO! HERE'S YOUR HOLY WATER");
                                                    //Destroy(gameObject);
            }
            else
            {
                print("SORRY, I HAVEN'T MADE IT YET");
            }
        }
        //if (Inventory.instance.HasItem(orderItem))      //  check if the player has the product that npc wants or not          
        //{
        //    Inventory.instance.RemoveItem(orderItem);
        //    getProduct = true;                  // npc gets product
        //    orderBubble.SetActive(false);       // hide UI
        //    ScoreManager.instance.AddScore();   // add score for player
        //    //print("THERE YOU GO! HERE'S YOUR HOLY WATER");
        //    //Destroy(gameObject);
        //}
        //else
        //{
        //    print("SORRY, I HAVEN'T MADE IT YET");
        //}
    }

    public void SwitchState(NPC_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void CountdownPatience()
    {
        // decreasing emotion value as the time goes by
        currentEB -= decSpeed * Time.deltaTime;
        UpdateEmoBar(currentEB, maxEB);

        // check if current emotion value reaches 0 or not, if yes, then isUpset = true
        isUpset = currentEB <= 0 ? true : false;
    }

    public void HideOrder()
    {
        orderBubble.SetActive(false);
    }

    public void Praying()
    {        
        prayingTime -= Time.deltaTime;
    }

    public void UpdateEmoBar(float currentValue, float maxValue)
    {
        emoBar.value = currentValue / maxValue;
    }

    public void Leave()
    {
        orderBubble.SetActive(false);
        Destroy(gameObject);
    }

    public void OrderProduct()
    {
        orderItem = itemLists[Random.Range(0, itemLists.Length)];
        orderBubble.transform.GetChild(1).GetComponent<Image>().sprite = orderItem.itemSprite;
        orderBubble.SetActive(true);
    }

    public void NormalEffect()
    {
        sprite.color = Color.white;
        //print("TODAY IS A GOOD DAY, EH?");
    }

    public void RainyEffect()
    {
        sprite.color = Color.blue;
        //print("OH! RAIN! RUN!");
    }

    public void SunnyEffect()
    {
        sprite.color = Color.red;
        //print("ITS SO DAMN HOT MANN");
    }

    public void LeaveScene()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position + transOffset;
        Gizmos.DrawLine(position, position + Vector3.right * detectLength);
    }

    public void OnNotify(WeatherType weather)
    {
        switch (weather)
        {
            case (WeatherType.Rainy):
                RainyEffect();
                return;

            case (WeatherType.Normal):
                NormalEffect();
                return;

            case (WeatherType.Sunny):
                SunnyEffect();
                return;

            default:
                return;
        }
    }

    void OnEnable()
    {
        weatherManager.AddObserver(this);
    }

    void OnDisable()
    {
        weatherManager.RemoveObserver(this);
    }
}
