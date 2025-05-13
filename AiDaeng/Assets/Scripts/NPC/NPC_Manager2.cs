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
    public Transform spawnPoint; // a position to spawn npc
    public Waypoints[] waypoints; // Waypoints to assign    

    private int spawnedNpcCount; // store total of npc that have spawned
    public int leftNpcCount; // store total of npc that have left 

    private GameManager gameManager;

    private void OnEnable()
    {        
        EventManager.TimerStop += OnTimeout;
    }

    private void OnDisable()
    {
        EventManager.TimerStop -= OnTimeout;
    }
    
    private void OnTimeout() => isTimeout = true;

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
        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
        NPC2 npcMovement = npc.GetComponent<NPC2>();
        if (npcMovement != null)
        {
            npcMovement.SetWaypoints(waypoints); // Assign waypoints to NPC            
        }
        spawnedNpcCount++;
        
    }

    //private IEnumerator ProcessQueue()
    //{
    //    yield return new WaitForSeconds(2f); // Small delay for spacing
    //}
}
