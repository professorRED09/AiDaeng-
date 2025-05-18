using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC2 : MonoBehaviour, IObserver
{
    [Header("Setting")]
    [SerializeField] float maxEB; // max value of emotion value
    public float decSpeed; // decreasing speed of emotion value, assign as public for adjusting in other script
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
    [SerializeField] GameObject thinkingText;
    [SerializeField] GameObject itemImage;
    [SerializeField] Items[] itemLists;

    [Header("Waypoints")]
    public Waypoints[] waypoints; // Store assigned waypoints
    public Waypoints[] wayToCounter;
    public Waypoints[] wayToPrey;
    public Waypoints[] wayToLeave;
    public Waypoints[] wayToRageLeave;

    public int currentWaypointIndex = 0;
    public bool isMoving;

    SpriteRenderer sprite;

    Inventory inventory;
    Subject weatherManager;

    [Header("Boolean")]
    public bool isUpset = false;
    public bool getProduct = false;
    public bool isClickable;
    public bool isWaiting;    

    [SerializeField] Items orderItem;
    PlayerController1 player;
    Namwan namwan;

    //FSM Stuff
    NPC_BaseState currentState;
    public NPC_ArriveState arriveState = new NPC_ArriveState();
    public NPC_OrderState orderState = new NPC_OrderState();
    public NPC_PreyState preyState = new NPC_PreyState();
    public NPC_LeaveState leaveState = new NPC_LeaveState();
    public NPC_RageLeaveState rageLeaveState = new NPC_RageLeaveState();

    private Animator animator;
    private NPC_Manager2 npcManager;
    bool isFacingRight = true;
    public Vector3 direction;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        inventory = Inventory.instance;
        weatherManager = FindObjectOfType<WeatherManager>();
        player = FindObjectOfType<PlayerController1>();
        namwan = FindObjectOfType<Namwan>();
        npcManager = FindObjectOfType<NPC_Manager2>();
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
        Animate(); // animate the character sprite
        CountdownPatience();

        if (direction.x > 0 && !isFacingRight || direction.x < 0 && isFacingRight) // check condition to flip the character sprite
        {
            Flip();
        }

        WaitForQueue();
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectLength, npcMask);

        //if (hit.collider == null)
        //{
        //    isMoving = true;
        //}
        //else
        //{
        //    isMoving = false;
        //}

        currentState.UpdateState(this);

        //IsSomeoneAhead();
        if (isMoving && currentWaypointIndex < waypoints.Length)
        {
            MoveToWaypoint();
        }
        else
        {
            isMoving = false;
        }
    }

    void WaitForQueue()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectLength, npcMask);

        if (hit.collider == null)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    //bool IsSomeoneAhead()
    //{
    //    // check what dir npc is moving
    //    // if it's up&down, use another raycast
    //    // if it's left&right, use another as well
    //    RaycastHit2D hit;
    //    if (direction.x != 0)
    //    {
    //        hit = Physics2D.Raycast(transform.position + transOffset, direction, detectLength, npcMask);
    //    }else if (direction.y != 0)
    //    {
    //        hit = Physics2D.Raycast(transform.position + transOffset, direction, detectLength, npcMask);
    //    }        
        
    //    //if(hit.collider == null)
    //    //{
    //    //    return false;
    //    //}
    //    //else
    //    //{
    //    //    return true;
    //    //}
    //}

    private void MoveToWaypoint()
    {        
        Waypoints targetWaypoint = waypoints[currentWaypointIndex];
        direction = (targetWaypoint.transform - transform.position).normalized;
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
                Inventory.instance.RemoveItem(orderItem);   // remove the given item from player's inventory
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
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            // called Namwan to give some water cups for this npc
            print("CALLED NAMWAN");
        }
    }

    
    public void SwitchState(NPC_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void ResetEmoBar()
    {
        currentEB = maxEB;
        UpdateEmoBar(currentEB, maxEB);
    }
    public void CountdownPatience()
    {
        if (!isWaiting)
            return;
        // decreasing emotion value as the time goes by
        currentEB -= decSpeed * Time.deltaTime;
        UpdateEmoBar(currentEB, maxEB);

        // check if current emotion value reaches 0 or not, if yes, then isUpset = true
        isUpset = currentEB <= 0 ? true : false;
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

    public void UpdateEmoBar(float currentValue, float maxValue)
    {
        emoBar.value = currentValue / maxValue;
    }

    // use in LeaveState when the npc leaves the area
    //public void Leave()
    //{
    //    orderBubble.SetActive(false);
    //    Destroy(gameObject);
    //}

    public void ThinkOfOrder()
    {
        //orderBubble.SetActive(true);
        orderBubble.transform.GetChild(0).GetComponent<Image>().enabled = true; // enable panel image
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = true; // enable "..." text;
        StartCoroutine(OrderProduct());// set text active
        ResetEmoBar();
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
    //public void OrderProduct()
    //{
    //    orderItem = itemLists[Random.Range(0, itemLists.Length)];   // Random order items for npc to order player
    //    orderBubble.transform.GetChild(1).GetComponent<Image>().sprite = orderItem.itemSprite;
    //    orderBubble.SetActive(true);
    //}

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
