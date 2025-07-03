using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC2 : MonoBehaviour, IObserver
{
    [Header("Setting")]
    private float maxMV = 10f;   // MV => mood value
    private float currentMV;

    private float moodDecSp = 0.4f;      // decreasing speed of mood value, assign as public for adjusting in other script
    private float tempMoodDecSp;
    //public float prayingTime = 2;
    private float speed = 5f;
    public GameObject happyVFX;
    //public GameObject scorePopup;
 
    //[SerializeField] Vector3 transOffset;
    [SerializeField] float detectLength;
    public LayerMask npcMask;

    [Header("UI")]
    [SerializeField] GameObject orderBubble;
    [SerializeField] Slider moodBar;
    [SerializeField] GameObject thinkingText;
    //[SerializeField] GameObject itemImage;
    [SerializeField] Items[] itemLists;
    //public List<Items> orderList;
    public List<GameObject> orderSlotLists;
    public GameObject orderSlot;
    public GameObject orderPanel;

    [Header("Waypoints")]
    public Transform[] waypoints; // Store assigned waypoints
    public Transform[] wayToCounter;    
    public Transform[] wayToPray;
    public Transform[] wayToLeave;
    public Transform[] wayToRageLeave;

    public int currentWaypointIndex = 0;
    

    SpriteRenderer sprite;

    [Header("Boolean")]
    public bool isMoving;
    public bool isUpset = false;
    public bool getProduct = false;
    public bool isClickable;
    public bool isWaiting;

    [SerializeField] Items orderItem;
    private PlayerController1 player;
    private Namwan namwan;

    //FSM Stuff
    public NPC_BaseState currentState;
    public NPC_ArriveState arriveState = new NPC_ArriveState();
    public NPC_WaitToOrderState waitToOrderState = new NPC_WaitToOrderState();
    public NPC_OrderState orderState = new NPC_OrderState();
    public NPC_PrayState prayState = new NPC_PrayState();
    public NPC_LeaveState leaveState = new NPC_LeaveState();
    public NPC_RageLeaveState rageLeaveState = new NPC_RageLeaveState();

    private Animator animator;
    private NPC_Manager2 npcManager;
    bool isFacingRight = true;
    private Vector3 direction;

    public Transform[] orderSpots;
    public Transform takenOrderSpot;

    private WeatherManager weatherManager;
    private ScoreManager scoreManager;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 

        weatherManager = FindObjectOfType<WeatherManager>();
        player = FindObjectOfType<PlayerController1>();
        namwan = FindObjectOfType<Namwan>();
        npcManager = FindObjectOfType<NPC_Manager2>();

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Start()
    {
        // set initial value       
        currentMV = maxMV;
        UpdateMoodBar(currentMV, maxMV);

        // set initial state to begin with
        currentState = arriveState;
        currentState.EnterState(this);

        // set up waypoint array
        wayToCounter = npcManager.wpToQueue;
        orderSpots = npcManager.wpToOrder;
        wayToPray = npcManager.wpToPray; 
        wayToLeave = npcManager.wpToLeave;
        wayToRageLeave = npcManager.wpToRageLeave;

        // store default value in temp value
        tempMoodDecSp = moodDecSp;

        // make npc recognize the weather when get spawned
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

        // if npc isn't move, means that they are waiting 
        if (!isMoving)
        {
            DecreaseMoodBar();
        }
        

        // check condition to flip the character sprite
        if (direction.x > 0 && !isFacingRight || direction.x < 0 && isFacingRight) 
        {
            Flip();
        }

        // check if there's other npc ahead of them
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
            for (int i = 0; i < orderSlotLists.Count; i++)
            {
                Items m_OrderItem = orderSlotLists[i].GetComponent<OrderSlot>().orderItem;
                // check if there's the exact order in the list, remove it 
                if (Inventory.instance.HasItem(m_OrderItem))
                {
                    // remove the given item from player's inventory
                    Inventory.instance.RemoveItem(m_OrderItem);
                    currentMV += maxMV/3.5f;
                    // remove the given item from npc's order panel and destroy its gameobject;
                    orderSlotLists[i].SetActive(false);
                    orderSlotLists.Remove(orderSlotLists[i]);

                }
                else
                {
                    NotificationManager.instance.AddNoti("คุณยังไม่ได้ทำออเดอร์เหล่านั้น");
                }    
            }

            if (orderSlotLists.Count == 0)
            {
                getProduct = true;                
                scoreManager.AddScore(transform.position); // FIX:implement on getting score base on current Mood Value
                GameObject m_HappyVFX = Instantiate(happyVFX, transform) as GameObject;
                m_HappyVFX.transform.parent = transform;
            }

        }
    }



    public void ThinkOfOrder()
    {
        orderPanel.SetActive(true);
        
        orderBubble.transform.GetChild(0).GetComponent<Image>().enabled = true; // enable panel image
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = true; // enable "..." text;
        StartCoroutine(OrderProduct()); // set text active
        //ResetMoodBar();
        //// play animation
    }

    IEnumerator OrderProduct()
    {
        yield return new WaitForSeconds(3);
        // random total of things to order (for now let's just random between 1 and 2)
        int orderNumbers = Random.Range(1, 3);  // npc will order 1 or 2 things (3 is the exclusive)
        
        for (int i = 0; i < orderNumbers; i++)
        {
            //random order
            Items order = itemLists[Random.Range(0, itemLists.Length)];
            //instantiate slot
            GameObject m_OrderSlot = Instantiate(orderSlot, orderPanel.transform);
            //set order item in order slot
            m_OrderSlot.GetComponent<OrderSlot>().SetOrderItem(order);
            orderSlotLists.Add(m_OrderSlot);
        }
        // disable "..." text;
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = false; 
        isWaiting = true;        
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
        //print(state);
        currentState = state;
        state.EnterState(this);
    }

    public void ResetMoodBar()
    {
        currentMV = maxMV;
        UpdateMoodBar(currentMV, maxMV);
    }

    public void DecreaseMoodBar()
    {
        if (isWaiting == false)
            return;

        // decreasing emotion value as the time goes by
        currentMV -= moodDecSp * Time.deltaTime;
        UpdateMoodBar(currentMV, maxMV);

        if (currentMV <= 0)
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
    //public void Praying()
    //{        
    //    prayingTime -= Time.deltaTime;
    //}

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

    

    public void NormalEffect()
    {
        sprite.color = Color.white;
        moodDecSp = tempMoodDecSp; 
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
        moodDecSp = 1;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;        
        Gizmos.DrawLine(transform.position, direction * detectLength);
        Gizmos.DrawLine(transform.position, transform.up);
    }

    void OnWeatherChange()
    {
        if(weatherManager.currentState == weatherManager.sunnyState)
        {
            SunnyEffect();
        }
        else if (weatherManager.currentState == weatherManager.rainyState)
        {
            RainyEffect();
        }
        else
        {
            NormalEffect();
        }
    }

    public void OnGettingProduct()
    {        
        isWaiting = false;
        takenOrderSpot.GetComponent<OrderSpot>().isFree = true;
        SwitchState(prayState);
    }

    //public IEnumerator OnGettingProduct(float waitTime)
    //{        
    //    GameObject m_ScorePopup = Instantiate(scorePopup, transform);
    //    m_ScorePopup.transform.parent = transform;
    //    yield return new WaitForSeconds(waitTime);
    //    print("STROP");
    //    StopAllCoroutines();
    //    isWaiting = false;
    //    takenOrderSpot.GetComponent<OrderSpot>().isFree = true;        
    //    SwitchState(prayState); 
    //}    

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
        animator.SetFloat("currentMood", currentMV);
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    void Flip()
    {
        if (!isMoving)
        {
            return;
        }
            

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
