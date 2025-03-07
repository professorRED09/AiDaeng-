using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    //public List<Transform> waypoints;
    public int npcCount = 3;

    public Queue<GameObject> npcQueue = new Queue<GameObject>();

    void Start()
    {
        print("asd");
        SpawnNPCs();
    }

    private void SpawnNPCs()
    {
        print("SPAWN");
        for (int i = 0; i < npcCount; i++)
        {
            GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);            
        }

        StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue()
    {
        while (npcQueue.Count > 0)
        {
            GameObject npc = npcQueue.Dequeue();
            yield return new WaitForSeconds(2f); // Small delay for spacing
        }
    }
}
