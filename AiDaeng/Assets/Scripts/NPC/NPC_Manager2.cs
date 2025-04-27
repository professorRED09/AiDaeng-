using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager2 : MonoBehaviour
{
    public float timer;
    public float currentTimer;
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public Waypoints[] waypoints; // Waypoints to assign
    //public int npcCount = 3;


    void Start()
    {
        SpawnNPCs();
        currentTimer = timer;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0)
        {
            SpawnNPCs();
            currentTimer = timer;
        }        
    }

    public void CountdownTimer()
    {

    }

    private void SpawnNPCs()
    {
        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
        NPC2 npcMovement = npc.GetComponent<NPC2>();
        if (npcMovement != null)
        {
            npcMovement.SetWaypoints(waypoints); // Assign waypoints to NPC            
        }
    }

    //private IEnumerator ProcessQueue()
    //{
    //    yield return new WaitForSeconds(2f); // Small delay for spacing
    //}
}
