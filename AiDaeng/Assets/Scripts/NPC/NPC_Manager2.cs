using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager2 : MonoBehaviour
{
    private float spawnTimer; // a spawn timer
    [SerializeField] float maxTime; // a longest period to spawn the next npc 
    [SerializeField] float minTime; // a shortest period to spawn the next npc
    [SerializeField] float currentSpawnTimer;
    private bool isTimeout = false;

    public GameObject npcPrefab;
    public GameObject spawnPoint; // a position to spawn npc

    [Header("Waypoints")]
    public Transform[] wpToQueue;       // Waypoints to queue 
    public Transform[] wpToOrder;       // Waypoints to order 
    public Transform[] wpToPray;        // Waypoints to prey 
    public Transform[] wpToLeave;       // Waypoints to leave
    public Transform[] wpToRageLeave;   // Waypoints to rage leave

    private int spawnedNpcCount; // store total of npc that have spawned
    public int leftNpcCount; // store total of npc that have left 

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();        
    }

    void Start()
    {
        SpawnNPCs();
        RandomSpawnTime();
    }

    private void Update()
    {
        currentSpawnTimer -= Time.deltaTime;
        if(currentSpawnTimer <= 0 && !isTimeout)
        {            
            SpawnNPCs();
            RandomSpawnTime();
        }

        if(leftNpcCount == spawnedNpcCount) 
        {
            if (!isTimeout)
            {
                return;
            }
            else
            {
                gameManager.ShowResult();// show game result
            }
        }
        
        
    }

    public void RandomSpawnTime()
    {
        currentSpawnTimer = Random.Range(minTime, maxTime);        
    }

    private void SpawnNPCs()
    {
        // if npc is standing on the spawn waypoint, don't spawn 
        GameObject npc = Instantiate(npcPrefab, spawnPoint.transform.position, Quaternion.identity);
        NPC2 npcMovement = npc.GetComponent<NPC2>();
        if (npcMovement != null)
        {
            npcMovement.SetWaypoints(wpToQueue); // Assign waypoints to NPC            
        }
        spawnedNpcCount++;
        
    }

    private void OnEnable()
    {
        EventManager.TimerStop += OnTimeout;
    }

    private void OnDisable()
    {
        EventManager.TimerStop -= OnTimeout;
    }

    private void OnTimeout() => isTimeout = true;

    //private IEnumerator ProcessQueue()
    //{
    //    yield return new WaitForSeconds(2f); // Small delay for spacing
    //}
}
