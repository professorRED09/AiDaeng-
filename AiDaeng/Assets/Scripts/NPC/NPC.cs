//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class NPC : MonoBehaviour, IObserver
//{
//    [Header("UI")]     
//    [SerializeField] GameObject orderBubble;
//    [SerializeField] Slider emoBar;
//    [SerializeField] Items[] itemLists;

//    //public Waypoints[] waypoints;
//    public Waypoints[] wayToCounter;

//    public bool isUpset = false;
//    public bool getProduct = false;
//    [SerializeField] bool canMove;

//    [SerializeField] Items orderItem;

//    public Transform counterWP;
//    public Transform preyWP;
//    public Transform[] leaveWP;

//    Inventory inventory;
//    Subject weatherManager;

//    [Header("Setting")]
//    [SerializeField] float maxEB; // max value of emotion value
//    [SerializeField] float decSpeed; // decreasing speed of emotion value
//    [SerializeField] float speed;
//    private float currentEB; // max value of emotion value
//    [SerializeField] Vector3 transOffset;
//    [SerializeField] float detectLength;
//    //private Vector3 origin;
//    [SerializeField] LayerMask npcMask;
    

//    //FSM Stuff
//    NPC_BaseState currentState;
//    public NPC_ArriveState arriveState = new NPC_ArriveState();
//    public NPC_OrderState orderState = new NPC_OrderState();
//    public NPC_PreyState preyState = new NPC_PreyState();
//    public NPC_LeaveState leaveState = new NPC_LeaveState();

//    private void Awake()
//    {
//        inventory = Inventory.instance;
//        weatherManager = FindObjectOfType<WeatherManager>();
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        // set initial value       
//        currentEB = maxEB;
//        UpdateEmoBar(currentEB, maxEB);

//        // set initial state to begin with
//        currentState = arriveState;
//        currentState.EnterState(this);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        RaycastHit2D hit = Physics2D.Raycast(transform.position + transOffset, Vector2.right, detectLength, npcMask);

//        if (hit.collider == null)
//        {
//            canMove = true;
//            //print("OH! THERE IS SOMEONE AHEAD OF ME");

//        }
//        else
//        {
//            canMove = false;
//            //print("MOVE");
//            //transform.position += direction * speed * Time.deltaTime;
//        }
//        currentState.UpdateState(this);
//    }

//    public void OrderAndWait()
//    {

//    }

//    public void SwitchState(NPC_BaseState state)
//    {
//        currentState = state;
//        state.EnterState(this);
//    }

//    //public void WaitingQueue(Vector3 direction)
//    //{
//    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectLength, npcMask);
//    //    if (hit.collider != null)
//    //    {
//    //        print("THERE ARE PEOPLE AHEAD OF ME, I'LL WAIT THEN");
//    //        return;
//    //    }
//    //}

//    public void WalkToWaypoint()
//    {

//    }

//    //public void WalkToCounter()
//    //{
//    //    if (!canMove)
//    //        return;
//    //    //Vector3 direction = (counterWP.position - transform.position).normalized;
//    //    //transform.position += direction * speed * Time.deltaTime;

//    //    Vector3 direction = (waypoints[1].transform - transform.position).normalized;
//    //    transform.position += direction * speed * Time.deltaTime;
//    //}

//    public void WalkToCounter()
//    {
//        if (!canMove)
//            return;
//        //Transform targetWP = 
//        Vector3 direction = (wayToCounter[1].transform - transform.position).normalized;
//        transform.position += speed * Time.deltaTime * direction;
//        if (Vector3.Distance(transform.position, wayToCounter[1].transform) <= 0.1f)
//        {
//            transform.position = wayToCounter[1].transform;
//            direction = (wayToCounter[0].transform - transform.position).normalized;            
//            transform.position += speed * Time.deltaTime * direction;
//            print(direction);
//        }
        
//    }

//    private void OnMouseDown()
//    {
//        //  check if the player has the product that npc wants or not
//        //  if yes, then switch state    
//        if (Inventory.instance.HasItem(orderItem))
//        {
//            Inventory.instance.RemoveItem(orderItem);
//            getProduct = true;
//            orderBubble.SetActive(false);
//            print("THERE YOU GO! HERE'S YOUR HOLY WATER");
//            Destroy(gameObject);
//        }
//        else
//        {
//            print("SORRY, I HAVEN'T MADE IT YET");
//        }
//    }

//    public void OnNotify(WeatherType weather)
//    {
//        switch (weather)
//        {
//            case (WeatherType.Normal):
//                print("TODAY IS A GOOD DAY TO PREY!");
//                return;

//            case (WeatherType.Sunny):
//                print("TODAY IS JUST SO HOT MAN!");
//                return;

//            default:
//                return;
//        }
//    }

//    public void SetWaypoints(List<Transform> assignedWaypoints)
//    {
//        //waypoints = assignedWaypoints;
//        //currentWaypointIndex = 0;
//        //isMoving = waypoints.Count > 0;
//    }

//    public void WalkToPrey()
//    {

//    }

//    public void CountdownPatience()
//    {
//        // decreasing emotion value as the time goes by
//        currentEB -= decSpeed * Time.deltaTime;
//        UpdateEmoBar(currentEB, maxEB);

//        // check if current emotion value reaches 0 or not, if yes, then isUpset = true
//        isUpset = currentEB <= 0 ? true : false;
//    }

//    public void UpdateEmoBar(float currentValue, float maxValue)
//    {
//        emoBar.value = currentValue / maxValue;
//    }

//    public void MoveToWaypoints()
//    {

//    }
//    //private void MoveToWaypoint(Waypoints waypoint)
//    //{
//    //    Transform targetWaypoint = waypoints[currentWaypointIndex];
//    //    Vector3 direction = (targetWaypoint.position - transform.position).normalized;
//    //    transform.position += direction * speed * Time.deltaTime;

//    //    if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
//    //    {
//    //        transform.position = targetWaypoint.position; // Snap to exact position            
//    //        currentWaypointIndex++;

//    //        if (currentWaypointIndex >= waypoints.Count)
//    //        {
//    //            canMove = false; // Stop moving when all waypoints are reached
//    //        }
//    //    }
//    //}

//    public void OrderProduct()
//    {
//        orderItem = itemLists[Random.Range(0, itemLists.Length)];
//        orderBubble.transform.GetChild(1).GetComponent<Image>().sprite = orderItem.itemSprite;        
//        orderBubble.SetActive(true);        
//    }

//    //private void OnDrawGizmosSelected()
//    //{        
//    //    Gizmos.color = Color.red;
//    //    Vector3 position = transform.position + transOffset;
//    //    //Vector3 direction = (counterWP.position - transform.position).normalized;
//    //    Vector3 direction = (waypoints[1].transform - transform.position).normalized;
//    //    Gizmos.DrawLine(position, position + direction * detectLength);    

//    //    // Draw lines in four directions
//    //    //Gizmos.DrawLine(position, position + Vector3.up * detectLength);    // Up
//    //    //Gizmos.DrawLine(position, position + Vector3.down * detectLength);  // Down
//    //    //Gizmos.DrawLine(position, position + Vector3.left * detectLength);  // Left
//    //    //Gizmos.DrawLine(position, position + Vector3.right * detectLength); // Right
//    //}
//}
