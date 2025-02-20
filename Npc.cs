using UnityEngine;

public class Npc : MonoBehaviour
{
    public string npcName;

    public Npc(string name)
    {
        npcName = name;
    }

    public static Npc GetGuestSequential()
    {
        GameObject npcObject = new GameObject("GuestNpc");
        Npc newNpc = npcObject.AddComponent<Npc>();
        newNpc.npcName = "Guest_" + Random.Range(1, 100);
        return newNpc;
    }
}
