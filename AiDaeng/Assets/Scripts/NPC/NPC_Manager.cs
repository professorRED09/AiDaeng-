using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Manager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] GameObject orderBubble;
    [SerializeField] Slider emoBar;
    [SerializeField] Items[] itemLists;
    public bool isUpset = false;
    Items orderItem;
    public Transform counterWP;
    public Transform preyWP;
    public Transform[] leaveWP;

    [Header("Setting")]
    [SerializeField] float maxEB; // max value of emotion value
    [SerializeField] float decSpeed; // decreasing speed of emotion value
    [SerializeField] float speed;
    private float currentEB; // max value of emotion value

    //FSM Stuff
    NPC_BaseState currentState;
    public NPC_ArriveState arriveState = new NPC_ArriveState();
    public NPC_OrderState orderState = new NPC_OrderState();
    public NPC_LeaveState leaveState = new NPC_LeaveState();
    
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
        Vector3 direction = (counterWP.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;        
    }

    private void OnMouseDown()
    {
        //  check if the product that player give is the product that npc wants
        //  if yes, then switch state        
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
