using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public GameObject[] npcs;
    public List<Transform> waypoints; // Assign in the Inspector
    public int npcCount = 3; // Number of NPCs to spawn

    private List<GameObject> activeNPCs = new List<GameObject>();

    void Start()
    {
        SpawnNPCs();
    }

    private void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)
        {
            GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
            //NPC npcMovement = npc.GetComponent<NPC>();
            //if (npcMovement != null)
            //{
            //    //npcMovement.SetWaypoints(new List<Transform>(waypoints)); // Assign waypoints
            //}
            activeNPCs.Add(npc);
        }
    }
}
