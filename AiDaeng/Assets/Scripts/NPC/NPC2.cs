using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC2 : MonoBehaviour, IObserver
{
    [Header("Setting")]
    [SerializeField] float maxEB;   // max value of emotion value
    public float decSpeed;         // decreasing speed of mood value, assign as public for adjusting in other script
    public float prayingTime = 2;

    public float speed = 2f;
    private float currentEB; // max value of emotion value
    [SerializeField] Vector3 transOffset;
    [SerializeField] float detectLength;
    public LayerMask npcMask;

    [Header("UI")]
    [SerializeField] GameObject orderBubble;
    [SerializeField] Slider moodBar;
    [SerializeField] GameObject thinkingText;
    [SerializeField] GameObject itemImage;
    [SerializeField] Items[] itemLists;

    [Header("Waypoints")]
    public Transform[] waypoints; // Store assigned waypoints
    public Transform[] wayToCounter;    
    public Transform[] wayToPrey;
    public Transform[] wayToLeave;
    public Transform[] wayToRageLeave;

    public int currentWaypointIndex = 0;
    public bool isMoving;

    SpriteRenderer sprite;

    [Header("Boolean")]
    public bool isUpset = false;
    public bool getProduct = false;
    public bool isClickable;
    public bool isWaiting;

    [SerializeField] Items orderItem;
    PlayerController1 player;
    Namwan namwan;

    //FSM Stuff
    public NPC_BaseState currentState;
    public NPC_ArriveState arriveState = new NPC_ArriveState();
    public NPC_WaitToOrderState waitToOrderState = new NPC_WaitToOrderState();
    public NPC_OrderState orderState = new NPC_OrderState();
    public NPC_PreyState preyState = new NPC_PreyState();
    public NPC_LeaveState leaveState = new NPC_LeaveState();
    public NPC_RageLeaveState rageLeaveState = new NPC_RageLeaveState();

    private Animator animator;
    private NPC_Manager2 npcManager;
    bool isFacingRight = true;
    public Vector3 direction;

    public Transform[] orderSpots;
    public Transform takenOrderSpot;

    private WeatherManager weatherManager;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 

        weatherManager = FindObjectOfType<WeatherManager>();
        player = FindObjectOfType<PlayerController1>();
        namwan = FindObjectOfType<Namwan>();
        npcManager = FindObjectOfType<NPC_Manager2>();
    }

    void Start()
    {
        // set initial value       
        currentEB = maxEB;
        UpdateMoodBar(currentEB, maxEB);

        // set initial state to begin with
        currentState = arriveState;
        currentState.EnterState(this);

        // set up waypoint array
        wayToCounter = npcManager.wpToQueue;
        orderSpots = npcManager.wpToOrder;
        wayToPrey = npcManager.wpToPrey; 
        wayToLeave = npcManager.wpToLeave;
        wayToRageLeave = npcManager.wpToRageLeave;

        OnWeatherChange();

    }   

    public void SetWaypoints(Transform[] assignedWaypoints)
    {
        waypoints = assignedWaypoints;
        if (waypoints.Length > 0)
        {
            //transform.position = waypoints[0].position; // Start at the first waypoint
            isMoving = true;
        }
    }

    void Update()
    {
        Animate(); // animate the character sprite
        DecreaseMoodBar();

        // check condition to flip the character sprite
        if (direction.x > 0 && !isFacingRight || direction.x < 0 && isFacingRight) 
        {
            Flip();
        }

        WaitForQueue();  

        currentState.UpdateState(this);

        if (isMoving && currentWaypointIndex < waypoints.Length && !isUpset)
        {
            MoveToWaypoint();
        }
        else if (isUpset)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    // Check if there's npc ahead, if yes then stop moving
    void WaitForQueue()
    {
        if (isUpset) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectLength, npcMask);
        //if (hit.collider.gameObject == null) return;
        //bool _isUpset = hit.collider.gameObject.GetComponent<NPC2>().isUpset;
        if (hit.collider == null)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void MoveToWaypoint()
    {
        //if (!isMoving) return;
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            transform.position = targetWaypoint.position; // Snap to exact position
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                isMoving = false; // Stop when all waypoints are reached                
            }
        }
    }

    // To give npc a product
    private void OnMouseDown()
    {
        if (!isClickable)
            return;
        player.MoveToWaypoint(player.counterWP);
        if (player.isAtCounter)
        {
            if (Inventory.instance.HasItem(orderItem))      //  check if the player has the product that npc wants or not          
            {
                Inventory.instance.RemoveItem(orderItem);   // remove the given item from player's inventory
                getProduct = true;                  // npc gets product
                orderBubble.SetActive(false);       // hide UI
                ScoreManager.instance.AddScore();   // add score for player
            }
            else
            {
                //print("SORRY, I HAVEN'T MADE IT YET");
            }
        }
    }

    // To call Namwan to give drink water to npc
    void OnMouseOver()
    {
        if (Input.GetMouseButton(1) && namwan != null)
        {
            namwan.MoveToWaypoint(transform);// called Namwan to give some water cups for this npc
            namwan.targetNPC = this;
        }
    }

    
    public void SwitchState(NPC_BaseState state)
    {
        print(state);
        currentState = state;
        state.EnterState(this);
    }

    public void ResetMoodBar()
    {
        currentEB = maxEB;
        UpdateMoodBar(currentEB, maxEB);
    }
    public void DecreaseMoodBar()
    {
        if (isWaiting == false)
            return;

        // decreasing emotion value as the time goes by
        currentEB -= decSpeed * Time.deltaTime;
        UpdateMoodBar(currentEB, maxEB);

        if (currentEB <= 0)
        {
            isUpset = true;
        }
    }

    // hide order bubble ui
    public void HideOrder()
    {
        orderBubble.SetActive(false);
    }

    // countdown when the character start praying
    public void Praying()
    {        
        prayingTime -= Time.deltaTime;
    }

    public void UpdateMoodBar(float currentValue, float maxValue)
    {
        moodBar.value = currentValue / maxValue;
    }

    
    public IEnumerator CheckOrderSpot()
    {
        for(int i = orderSpots.Length-1; i >= 0; i--)
        {
            Transform _freeSpot = orderSpots[i];
            OrderSpot freeSpot = orderSpots[i].GetComponent<OrderSpot>();
            if (freeSpot.isFree && takenOrderSpot == null)
            {
                takenOrderSpot = _freeSpot;//move to that spot
                freeSpot.isFree = false;
                break;                
            }
        }
        yield return null;
    }

    public void ThinkOfOrder()
    {
        //orderBubble.SetActive(true);
        orderBubble.transform.GetChild(0).GetComponent<Image>().enabled = true; // enable panel image
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = true; // enable "..." text;
        StartCoroutine(OrderProduct());// set text active
        ResetMoodBar();
        // play animation
    }

    IEnumerator OrderProduct()
    {
        yield return new WaitForSeconds(3);
        orderItem = itemLists[Random.Range(0, itemLists.Length)];   // Random order items for npc to order player
        orderBubble.transform.GetChild(1).GetComponent<Image>().sprite = orderItem.itemSprite;  // set order image
        orderBubble.transform.GetChild(1).GetComponent<Image>().enabled = true;     // enable order image 

        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = false; // disable "..." text;

        //itemImage.SetActive(true);   // enable item image on npc's order
        //thinkingText.SetActive(false);
        isWaiting = true;
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

    public void RageMood()
    {
        //LeaveScene();

        direction = Vector3.left;
        transform.position += speed * Time.deltaTime * direction;
        if (transform.position.x <= -10)
        {
            LeaveScene();
        }

    }

    public void LeaveScene()
    {
        npcManager.leftNpcCount++;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position + transOffset;
        Gizmos.DrawLine(position, position + Vector3.right * detectLength);
        Gizmos.DrawLine(position, position + Vector3.up * detectLength);
    }

    void OnWeatherChange()
    {
        if(weatherManager.currentState == weatherManager.sunnyState)
        {
            SunnyEffect();
        }else if (weatherManager.currentState == weatherManager.rainyState)
        {
            RainyEffect();
        }
        else
        {
            NormalEffect();
        }
    }

    public void OnNotify(Action action)
    {

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

    // animate character based on each conditions
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

    void OnEnable()
    {
        weatherManager.AddObserver(this);
    }

    void OnDisable()
    {
        weatherManager.RemoveObserver(this);
    }
}
