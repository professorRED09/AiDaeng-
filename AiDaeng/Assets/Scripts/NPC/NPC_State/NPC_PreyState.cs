using UnityEngine;

public class NPC_PreyState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isClickable = false;
        npc.currentWaypointIndex = 0;
        Debug.Log("THANKS, I'M MA GO PREYING");
        npc.SetWaypoints(npc.wayToPrey);
    }

    public override void UpdateState(NPC2 npc)
    {
        npc.Praying();
        if (npc.prayingTime <= 0)
            npc.SwitchState(npc.leaveState);

        if (Vector3.Distance(npc.transform.position, npc.wayToPrey[npc.wayToPrey.Length - 1].transform) <= 0.1f)
        {
            npc.transform.position = npc.wayToPrey[npc.wayToPrey.Length - 1].transform;
            

        }
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
