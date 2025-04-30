using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuddhaBasin : MonoBehaviour
{
    [Header("Ref")]
    public Transform waypoint;  // The waypoint for this task
    public int index;           // Index of this waypoint    
    private PlayerController1 player;    // Reference for player script 

    public static BuddhaBasin instance;

    [Header("UI")]
    public Slider waterBar;
    [SerializeField] GameObject fullBasin_Notif;

    [Header("Settings")]
    public bool isFull;    // check if the basin is full
    public float maxWB; // max quantity of water in the basin
    [SerializeField] float currentWB; // current quantity of water in the basin

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerController1>(); // get reference from player script
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetBasin(); // start the game with empty basin
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && player.targetWP == gameObject)
        {
            StartCoroutine(ResetBasin());
        }
    }

    IEnumerator ResetBasin()
    {
        yield return new WaitForSeconds(0.7f);
        currentWB = 0;
        isFull = false;
        UpdateWaterBar(currentWB, maxWB);
    }

    public void IncreaseWater(int quant)
    {
        currentWB += quant;
        if(currentWB >= maxWB)
        {
            currentWB = maxWB;
            isFull = true;
            OnFullBasin();// pop up notif window
        }
        UpdateWaterBar(currentWB, maxWB);

        print("WATER INCREASES");
    }

    public void UpdateWaterBar(float currentValue, float maxValue)
    {
        waterBar.value = currentValue / maxValue;
    }

    void OnFullBasin()
    {
        isFull = true;
        fullBasin_Notif.GetComponentInChildren<TMP_Text>().text = "น้ำเต็มแล้ว";
        fullBasin_Notif.GetComponent<Animator>().SetTrigger("isFull");
    }

    private void OnMouseDown()
    {
        // when players click on this task, the character will move to this task  
        if (waypoint != null && player != null)
        {
            player.MoveToWaypoint(waypoint);
            player.targetWP = gameObject;
            Debug.Log("MOVING TO TASK: " + gameObject.name);
        }
    }

}
