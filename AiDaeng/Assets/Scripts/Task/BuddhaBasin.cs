using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BuddhaBasin : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip taskSFX;
    public AudioClip clickSFX;
    public AudioClip fullSFX;

    public bool isFull;                 // check if the basin is full
    public float maxWB;                 // max quantity of water in the basin
    public float currentWB;   // current quantity of water in the basin
    public Transform waypoint;          // The waypoint for this task

    private PlayerController1 player;    // Reference for player script 
    private AudioSource audioSource;

    public static BuddhaBasin instance;
    private SpriteRenderer spriteRender;
    [SerializeField] private MixturePanel mixturePanel;

    [Header("UI")]
    //public Slider waterBar;
    public Image waterBarFill;
    [SerializeField] TMP_Text fullWaterText;


    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<PlayerController1>(); 
        spriteRender = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

        UpdateWaterBar(currentWB, maxWB);
        //StartCoroutine(ResetBasin());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && player.targetWP == gameObject)
        {
            if (currentWB != 0)
            {
                StartCoroutine(ResetBasin());                
            }            
        }
    }

    IEnumerator ResetBasin()
    {
        player.GetComponent<Animator>().SetBool("isDoingTask", true);

        audioSource.clip = taskSFX;
        audioSource.Play(); // play soundFX

        yield return new WaitForSeconds(0.7f);

        player.GetComponent<Animator>().SetBool("isDoingTask", false);

        currentWB = 0;
        isFull = false;
        UpdateWaterBar(currentWB, maxWB);

        fullWaterText.enabled = false;
    }

    public void IncreaseWater()
    {
        currentWB += 2.5f;
        //if(currentWB >= maxWB)
        //{
        //    currentWB = maxWB;
        //    isFull = true;
        //    OnFullBasin();// pop up notif window
        //}
        UpdateWaterBar(currentWB, maxWB);
        //print("WATER INCREASES");
    }

    public void UpdateWaterBar(float currentValue, float maxValue)
    {
        float targetFillAmount = currentValue / maxValue;
        waterBarFill.DOFillAmount(targetFillAmount, 2f);

        if (currentWB >= maxWB)
        {
            currentWB = maxWB;
            isFull = true;
            OnFullBasin();// pop up notif window
        }
    }

    void OnFullBasin()
    {
        isFull = true;

        // play soundFX
        audioSource.clip = fullSFX;
        audioSource.Play();

        fullWaterText.enabled = true;
        fullWaterText.gameObject.transform.DOMove(fullWaterText.gameObject.transform.position + Vector3.up, 1f);

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
            mixturePanel.CancelCurrentProduct();
            player.targetWP = gameObject;            
        }
    }

    private void OnMouseOver()
    {
        spriteRender.color = Color.black;
    }

    private void OnMouseExit()
    {
        spriteRender.color = Color.white;
    }

}
