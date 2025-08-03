using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWaterTask : MonoBehaviour, IObserver
{
    [Header("Ref")]
    public Transform waypoint;     
  
    private Namwan namwan;       
    private AudioSource audioSource;
    [SerializeField] private bool _isAvailable = true;
    private WeatherManager weatherManager;

    void Awake()
    {
        weatherManager = FindObjectOfType<WeatherManager>();
        namwan = FindObjectOfType<Namwan>(); 
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
            
            //Change current task for namwan
            namwan.ChangeTask(Namwan.CurrentTask.DrinkWater);
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
