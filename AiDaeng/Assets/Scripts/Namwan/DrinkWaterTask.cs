using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWaterTask : MonoBehaviour, IObserver
{
    [Header("Ref")]
    public Transform waypoint;              // The waypoint for this task
    /*public int index;  */                     // Index of this waypoint    
    private Namwan namwan;       // Reference for player script
    //public Items goalItem;
    private AudioSource audioSource;
    //[SerializeField] AudioClip soundFX;
    [SerializeField] private bool _isAvailable = true;
    private WeatherManager weatherManager;

    void Awake()
    {
        weatherManager = FindObjectOfType<WeatherManager>();
        namwan = FindObjectOfType<Namwan>(); // get reference from player script
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
    } 

    private void OnMouseDown()
    {
        // when players click on this task, the character will move to this task  
        if (waypoint != null && namwan != null)
        {
            namwan.MoveToWaypoint(waypoint);

            if (!_isAvailable)
            {
                Debug.Log("CANT PICK");
                return;
            }
            
            namwan.currentTask = Namwan.CurrentTask.DrinkWater;
            namwan.ChangeTask(namwan.currentTask);
        }
    }

    public void OnNotify(WeatherType weather)
    {
        switch (weather)
        {
            case (WeatherType.Rainy):
                _isAvailable = false;
                //Debug.Log("NAMWAN CAN'T PICK DRINK");
                return;               

            default:
                _isAvailable = true;
                //Debug.Log("NAMWAN CAN PICK DRINK");
                return;
        }
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
