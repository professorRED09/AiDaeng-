using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IObserver
{
    [Header("Ref")]     
    [SerializeField] GameObject orderBubble;
    [SerializeField] Slider emoBar;
    [SerializeField] Items[] itemLists;
    public Waypoints[] wayPoints;
    public bool isUpset = false;
    public bool getProduct = false;
    [SerializeField] Items orderItem;
    public Transform counterWP;
    public Transform preyWP;
    public Transform[] leaveWP;
    Inventory inventory;
    Subject weatherManager;

    [Header("Setting")]
    [SerializeField] float maxEB; // max value of emotion value
    [SerializeField] float decSpeed; // decreasing speed of emotion value
    [SerializeField] float speed;
    private float currentEB; // max value of emotion value

    //FSM Stuff
    NPC_BaseState currentState;
    public NPC_ArriveState arriveState = new NPC_ArriveState();
    public NPC_OrderState orderState = new NPC_OrderState();
    public NPC_PreyState preyState = new NPC_PreyState();
    public NPC_LeaveState leaveState = new NPC_LeaveState();

    private void Awake()
    {
        inventory = Inventory.instance;
        weatherManager = FindObjectOfType<WeatherManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // set initial value       
        currentEB = maxEB;
        UpdateEmoBar(currentEB, maxEB);

        // set initial state to begin with
        currentState = arriveState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void OrderAndWait()
    {

    }

    public void SwitchState(NPC_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void WalkToCounter()
    {
        Vector3 direction = (wayPoints[1].transform - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;        
    }

    private void OnMouseDown()
    {
        //  check if the player has the product that npc wants or not
        //  if yes, then switch state    
        if (Inventory.instance.HasItem(orderItem))
        {
            Inventory.instance.RemoveItem(orderItem);
            getProduct = true;
            orderBubble.SetActive(false);
            print("THERE YOU GO! HERE'S YOUR HOLY WATER");
        }
        else
        {
            print("SORRY, I HAVEN'T MADE IT YET");
        }
    }

    public void OnNotify(WeatherType weather)
    {
        switch (weather)
        {
            case (WeatherType.Normal):
                print("TODAY IS A GOOD DAY TO PREY!");
                return;

            case (WeatherType.Sunny):
                print("TODAY IS JUST SO HOT MAN!");
                return;

            default:
                return;
        }
    }

    public void SetWaypoints(List<Transform> assignedWaypoints)
    {
        //waypoints = assignedWaypoints;
        //currentWaypointIndex = 0;
        //isMoving = waypoints.Count > 0;
    }

    public void WalkToPrey()
    {

    }

    public void CountdownPatience()
    {
        // decreasing emotion value as the time goes by
        currentEB -= decSpeed * Time.deltaTime;
        UpdateEmoBar(currentEB, maxEB);

        // check if current emotion value reaches 0 or not, if yes, then isUpset = true
        isUpset = currentEB <= 0 ? true : false;             
    }

    public void UpdateEmoBar(float currentValue, float maxValue)
    {
        emoBar.value = currentValue / maxValue;
    }

    public void OrderProduct()
    {
        orderItem = itemLists[Random.Range(0, itemLists.Length)];
        orderBubble.transform.GetChild(1).GetComponent<Image>().sprite = orderItem.itemSprite;        
        orderBubble.SetActive(true);        
    }
}
