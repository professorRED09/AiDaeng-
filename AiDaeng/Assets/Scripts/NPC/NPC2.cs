using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NPC2 : MonoBehaviour, IObserver
{
    [Header("Setting")]
    private float maxMV = 10f;   // MV => mood value
    private float currentMV;

    private float moodDecSp ;      // decreasing speed of mood value, assign as public for adjusting in other script
    private float defaultMoodDecSp = 0.2f;
    //public float prayingTime = 2;
    private readonly float speed = 5f;
    public GameObject happyVFX;

    public float detectLength = 1.2f;
    public LayerMask npcMask;
    public int orderTotalForTutorial = 0;

    [Header("UI")]
    [SerializeField] GameObject orderBubble;
    //[SerializeField] GameObject moodBar;
    [SerializeField] Image _moodBarFill;
    [SerializeField] GameObject _emoji;

    //[SerializeField] GameObject thinkingText;
    //[SerializeField] GameObject itemImage;
    [SerializeField] List<Items> _itemLists;
    //public List<Items> orderList;
    public List<GameObject> orderSlotLists;
    public List<Items> orderLists;
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

    #region Finite State Machine
    public NPC_BaseState currentState;
    public NPC_ArriveState arriveState = new NPC_ArriveState();
    public NPC_WaitToOrderState waitToOrderState = new NPC_WaitToOrderState();
    public NPC_OrderState orderState = new NPC_OrderState();
    public NPC_PrayState prayState = new NPC_PrayState();
    public NPC_LeaveState leaveState = new NPC_LeaveState();
    public NPC_RageLeaveState rageLeaveState = new NPC_RageLeaveState();
    #endregion

    private Animator animator;
    private NPC_Manager2 npcManager;
    private bool isFacingRight = true;
    [SerializeField] private Vector3 direction;

    public Transform[] orderSpots;
    public Transform takenOrderSpot;

    private WeatherManager weatherManager;
    private ScoreManager scoreManager;

    private int _serveAttemp = 0;
    [SerializeField] GameObject _umbrellaIcon;
    public bool hasUmbrella;
    private UmbrellaTask _umbrellaTask;

    bool isRainLover = true;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _getWaterSFX;
    [SerializeField] AudioClip _getUmbrellaSFX;
    [SerializeField] AudioClip _thankSFX;

    private MixturePanel _mixturePanel;
    private Vector3 _originalScale; 
    private void Awake()
    {        
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        weatherManager = FindObjectOfType<WeatherManager>();
        player = FindObjectOfType<PlayerController1>();
        namwan = FindObjectOfType<Namwan>();
        npcManager = FindObjectOfType<NPC_Manager2>();

        scoreManager = FindObjectOfType<ScoreManager>();
        _umbrellaTask = FindObjectOfType<UmbrellaTask>();

        _mixturePanel = FindObjectOfType<MixturePanel>();        

        // set initial value       
        currentMV = maxMV;

        //random to determine if this npc is rain lover or not
        float randValue = Random.value;
        if (randValue < .65f) // 65% of the time
        {
            isRainLover = true;
            //Debug.Log("LOVE");
        }
        else // 35% of the time
        {
            isRainLover = false;
            //Debug.Log("HATE");
        }
    }

    void Start()
    {        
        UpdateMoodBar();

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
        //defaultMoodDecSp = moodDecSp;
        moodDecSp = defaultMoodDecSp;

        // make npc recognize the weather when get spawned
        OnWeatherChange();

        // store item lists from npc manager
        _itemLists.AddRange(npcManager.itemLists);

        _originalScale = orderBubble.transform.localScale;
    }   

    public void SetWaypoints(Transform[] assignedWaypoints)
    {
        waypoints = assignedWaypoints;
        Transform targetWaypoint = waypoints[0];
        direction = (targetWaypoint.position - transform.position).normalized;

        if (waypoints.Length > 0)
        {
            //transform.position = waypoints[0].position; // Start at the first waypoint
            isMoving = true;
        }
    }

    void Update()
    {
        Animate(); // animate the character sprite

        //OnWeatherChange();

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
            //SwitchState(rageLeaveState);
        }
        else
        {
            isMoving = false;
        }        
    }

    public void DoFade()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 2.5f);
        Color currentColor = sprite.color;        
        if (hit.collider == null)
        {
            currentColor.a = 1f;
        }
        else
        {
            currentColor.a = 0.5f;
        }

        sprite.color = currentColor;
    }

    public void CancelFade()
    {
        Color currentColor = sprite.color;
        currentColor.a = 1f;
        sprite.color = currentColor;
    }

    IEnumerator GetDrinkWaterBuff()
    {
        //float m_tempDecSp = moodDecSp;
        moodDecSp = 0.1f;
        print("AHH");           

        yield return new WaitForSeconds(5f);
        moodDecSp = defaultMoodDecSp;
    }

    public void OnGettingWater()
    {
        // NPC won't recieve drinking water on rainy weather
        if(weatherManager.currentState == weatherManager.rainyState)
        {
            Debug.Log("I'm not thirsty. It's already cold!");
            return;
        }

        StartCoroutine(GetDrinkWaterBuff());
        ResetMoodBar();

        GameObject m_HappyVFX = Instantiate(happyVFX, transform) as GameObject;
        m_HappyVFX.transform.parent = transform;

        GameObject m_waterBuff =  Instantiate(npcManager.waterBuff, transform) as GameObject;
        m_waterBuff.transform.SetParent(orderBubble.transform.GetChild(6));
        //orderBubble.transform.GetChild(6).gameObject.transform.SetParent(m_waterBuff.transform, false);

        _audioSource.clip = _getWaterSFX;
        _audioSource.Play();
    }

    public void OnGettingUmbrella()
    {
        NormalEffect();
        ResetMoodBar();

        //_umbrellaIcon.SetActive(true);
        hasUmbrella = true;

        GameObject m_umbrellaBuff = Instantiate(npcManager.umbrellaBuff, transform) as GameObject;
        m_umbrellaBuff.transform.SetParent(orderBubble.transform.GetChild(6));
        //m_umbrellaBuff.transform.parent = orderBubble.transform.GetChild(6).transform;

        _audioSource.clip = _getUmbrellaSFX;
        _audioSource.Play();
    }    

    // Check if there's npc ahead, if yes then stop moving
    void WaitForQueue()
    {
        if (isUpset /*|| getProduct*/) return;
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
        transform.position += speed * Time.deltaTime * direction;
        //WaitForQueue();
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
        {            
            return;
        }

        _mixturePanel.CancelCurrentProduct();
        player.MoveToWaypoint(player.counterWP);
        player.SetTargetNPC(this);        
    }

    public void CheckProductFromPlayer()
    {       
        //check if player has all of products
        bool playerHasAllProducts = true;        
        foreach (var element in orderSlotLists)
        {
            if (!Inventory.instance.items.Contains(element.GetComponent<OrderSlot>().orderItem))
            {
                playerHasAllProducts = false;
                break;
            }
        }

        //if player has all products for npc who has more than 1 order, give them all
        if (playerHasAllProducts && orderLists.Count > 1)
        {
            //Inventory.instance.items.RemoveAll(x => orderLists.Contains(x));
            for(int i = 0; i < orderLists.Count; i++)
            {
                Inventory.instance.items.Remove(orderLists[i]);
            }

            Inventory.instance.DisplayItems();

            //clear slot list
            orderSlotLists.Clear();
        }

        //if player has just 1 products OR npc orders only 1 products
        else if (!playerHasAllProducts || orderLists.Count < 2)
        {
            bool m_hasSomeOfProduct = false;
            for (int i = 0; i < orderSlotLists.Count; i++)
            {
                Items m_OrderItem = orderSlotLists[i].GetComponent<OrderSlot>().orderItem;
                if (Inventory.instance.HasItem(m_OrderItem))
                {
                    // remove the given item from player's inventory
                    Inventory.instance.RemoveItem(m_OrderItem);

                    m_hasSomeOfProduct = true;

                    currentMV += maxMV / 3.5f;
                    if (currentMV > maxMV)
                    {
                        currentMV = maxMV;
                    }

                    // remove the given item from npc's order panel and destroy its gameobject;
                    orderSlotLists[i].SetActive(false);
                    orderSlotLists.Remove(orderSlotLists[i]);

                    _serveAttemp += 1;
                }
            }

            // after checking loop, if player doesn't have any product that match npc's need, feedback to player
            if (!m_hasSomeOfProduct)
            {
                NotificationManager.instance.AddNoti("คุณยังไม่ได้ทำออเดอร์เหล่านั้น");
                //Vector3 _originalScale = orderBubble.transform.localScale;
                orderBubble.transform.DOScale(orderBubble.transform.localScale * 1.3f, 0.3f)
                            .SetEase(Ease.Flash)
                            .OnComplete(() =>
                            {
                                orderBubble.transform.DOScale(_originalScale, 0.3f);
                            });

                //if tutorial isn't null, call tutorial
                if(InteractiveTutorial.instance != null && Inventory.instance.items.Count != 0)
                {
                    InteractiveTutorial.instance.SwitchState(InteractiveTutorial.instance.onWrongNarmObState);
                }
                
            }
        }

        else
        {
            
        }

        

        if (orderSlotLists.Count == 0)
        {
            getProduct = true;

            if (_serveAttemp < 1)
            {
                print("BONUS");
                //player will get score base on npc's current Mood Value
                scoreManager.AddAllOrdersScore(transform.position, GetScoreBasedOnMood());
            }
            else
            {
                print("NO BONUS");
                //player will get score base on npc's current Mood Value
                scoreManager.AddScore(transform.position, GetScoreBasedOnMood());
            }


            //scoreManager.AddScore(transform.position, GetScoreBasedOnMood());z
            GameObject m_HappyVFX = Instantiate(happyVFX, transform) as GameObject;
            m_HappyVFX.transform.parent = transform;
        }        
    }

    public void GetPrayingScore()
    {
        scoreManager.AddScore(transform.position, GetScoreBasedOnMood());
        GameObject m_HappyVFX = Instantiate(happyVFX, transform) as GameObject;
        m_HappyVFX.transform.parent = transform;
    }

    public void PlayGetItemAnimation()
    {
        isClickable = false;
        //hide order bubble
        HideOrder();
        ResetMoodBar();
        //orderBubble.SetActive(false);
        orderBubble.transform.GetChild(0).GetComponent<Image>().enabled = false;     // disable panel image
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = false;  // disable "..." text;

        animator.Play("GetItem");
        _audioSource.clip = _thankSFX;
        _audioSource.Play();
    }

    public void OnGettingProduct()
    {
        //Debug.Log("OnGettingProduct");   

        //isWaiting = false;

        //ResetMoodBar();
        GameObject m_HappyVFX = Instantiate(happyVFX, transform) as GameObject;
        m_HappyVFX.transform.parent = transform;
        takenOrderSpot.GetComponent<OrderSpot>().isFree = true; 
        SwitchState(prayState);
    }

    public void ThinkOfOrder()
    {
        orderPanel.SetActive(true);
        
        orderBubble.transform.GetChild(0).GetComponent<Image>().enabled = true;     // enable panel image
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = true;  // enable "..." text;
        StartCoroutine(OrderProduct());                                             // set text active
        //ResetMoodBar();
        //// play animation
    }

    IEnumerator OrderProduct()
    {
        yield return new WaitForSeconds(3);

        int _orderNumbers = 0;
        // random total of things to order (for now let's just random between 1 and 2)
        if(orderTotalForTutorial == 0)
        {
            _orderNumbers = Random.Range(1, 3);  // npc will order 1 or 2 things (3 is the exclusive)
        }
        else
        {
            _orderNumbers = orderTotalForTutorial;
        }
        //int _orderNumbers = Random.Range(1, 3);  // npc will order 1 or 2 things (3 is the exclusive)

        isClickable = true;

        for (int i = 0; i < _orderNumbers; i++)
        {
            //random order
            Items order = _itemLists[Random.Range(0, _itemLists.Count)];

            //add to list
            orderLists.Add(order);

            //instantiate slot
            GameObject m_OrderSlot = Instantiate(orderSlot, orderPanel.transform);

            //set order item in order slot
            m_OrderSlot.GetComponent<OrderSlot>().SetOrderItem(order);
            orderSlotLists.Add(m_OrderSlot);
            _itemLists.Remove(order);
        }

        // disable "..." text;
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = false; 
        isWaiting = true;        
    }

    // To call Namwan to give drink water to npc
    void OnMouseOver()
    {        
        if (Input.GetMouseButtonDown(1) && namwan != null && !isMoving && namwan.inventory.canServe)
        {            
            namwan.MoveToWaypoint(transform);
            namwan.targetNPC = this;
        }
        else if (Input.GetMouseButtonDown(1) && !namwan.inventory.canServe)
        {
            //inform player 
            namwan.OnEmptyServe();
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
        UpdateMoodBar();
        //GameObject m_HappyVFX = Instantiate(happyVFX, transform) as GameObject;
        //m_HappyVFX.transform.parent = transform;
    }

    public void DecreaseMoodBar()
    {
        if (isWaiting == false)
            return;

        // decreasing emotion value as the time goes by
        currentMV -= moodDecSp * Time.deltaTime;
        UpdateMoodBar();

        if (currentMV <= 0)
        {
            isUpset = true;
        }
    }

    // hide order bubble ui
    public void HideOrder()
    {
        orderBubble.transform.GetChild(0).GetComponent<Image>().enabled = false;     // disable panel image
        orderBubble.transform.GetChild(2).GetComponent<TMP_Text>().enabled = false;  // disable "..." text;
        orderBubble.transform.GetChild(4).gameObject.SetActive(false);
        //orderBubble.SetActive(false);
    }
    
    public void UpdateMoodBar()
    {
        //if (getProduct) return;

        float targetFillAmount = currentMV / maxMV;
        _moodBarFill.fillAmount = targetFillAmount;
        //_moodBarFill.DOFillAmount(targetFillAmount, 0.5f);

        if(currentMV >= maxMV * 0.67)
        {
            _emoji.GetComponent<Animator>().Play("Happy");            
        }
        else if (currentMV >= maxMV * 0.33)
        {            
            _emoji.GetComponent<Animator>().Play("Edgy");
        }
        else
        {
            _emoji.GetComponent<Animator>().Play("Angry");
        }
 
    }

    public float GetScoreBasedOnMood()
    {        
        if (currentMV >= maxMV * 0.67)
        {
            return scoreManager.baseScore;
        }
        else if (currentMV >= maxMV * 0.33)
        {
            return scoreManager.baseScore * 0.8f;
        }
        else
        {
            return scoreManager.baseScore * 0.5f;
        }        
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
        defaultMoodDecSp = 0.2f;
        moodDecSp = defaultMoodDecSp; 
        //print("TODAY IS A GOOD DAY, EH?");
    }

    public void RainyEffect()
    {
        if (hasUmbrella) return;

        if (isRainLover)
        {
            defaultMoodDecSp = 0.1f;            
        }
        else
        {
            //Debug.Log("EEW RAIN");
            defaultMoodDecSp = 0.45f;
        }

        moodDecSp = defaultMoodDecSp;

        //set color to a cool tone
        sprite.color = new Vector4(0.48f, 0.53f, 0.76f, 1f);
        //Color currentColor = sprite.color;
        //currentColor = Color.blue;
        //currentColor.a = 0.5f;
        //sprite.color = currentColor;

        //moodDecSp = 1;
        //print("OH! RAIN! RUN!");
    }

    public void SunnyEffect()
    {
        if (hasUmbrella) return;       

        //set color to a hot tone
        sprite.color = new Vector4(1f, 0.83f, 0.62f, 1f);

        defaultMoodDecSp = 0.55f;
        moodDecSp = defaultMoodDecSp;
        //print("ITS SO DAMN HOT MANN");
    }

    public void OnRageMood()
    {        
        // make it go above an inch so it won't overlap with the other
        direction = Vector3.left;
        transform.position += speed * Time.deltaTime * direction;
        //print("RAGE");

        if (transform.position.x <= -10)
        {
            LeaveScene();
        }

    }

    public void LeaveScene()
    {
        npcManager.leftNpcCount++;

        if (hasUmbrella) _umbrellaTask.ReturnUmbrella();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * detectLength);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 2.5f);
        //Gizmos.DrawLine(transform.position, transform.up);
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
