using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuddhaBasin : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip taskSFX;
    public AudioClip clickSFX;

    public bool isFull;    // check if the basin is full
    public float maxWB; // max quantity of water in the basin
    [SerializeField] float currentWB; // current quantity of water in the basin
    public Transform waypoint;  // The waypoint for this task

    private PlayerController1 player;    // Reference for player script 
    private AudioSource audioSource;

    public static BuddhaBasin instance;

    [Header("UI")]
    public Slider waterBar;
    //[SerializeField] GameObject fullBasin_Notif;


    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerController1>(); // get reference from player script
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResetBasin());
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
        audioSource.clip = taskSFX;
        audioSource.Play(); // play soundFX
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
        //print("WATER INCREASES");
    }

    public void UpdateWaterBar(float currentValue, float maxValue)
    {
        waterBar.value = currentValue / maxValue;
    }

    void OnFullBasin()
    {
        isFull = true;
        NotificationManager.instance.AddNoti("น้ำเต็มแล้วน้อง!");        
    }

    private void OnMouseDown()
    {
        audioSource.clip = clickSFX;
        audioSource.Play(); // play soundFX
        // when players click on this task, the character will move to this task  
        if (waypoint != null && player != null)
        {
            player.MoveToWaypoint(waypoint);
            player.targetWP = gameObject;            
        }
    }

}
