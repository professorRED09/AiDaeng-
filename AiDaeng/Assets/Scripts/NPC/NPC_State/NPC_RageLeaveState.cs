using UnityEngine;

public class NPC_RageLeaveState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isClickable = false;
        npc.HideOrder();
        npc.currentWaypointIndex = 0;
        npc.SetWaypoints(npc.wayToRageLeave);        
    }

    public override void UpdateState(NPC2 npc)
    {
        if (Vector3.Distance(npc.transform.position, npc.wayToLeave[npc.wayToCounter.Length - 1].transform) <= 0.1f)
        {
            npc.LeaveScene();
        }
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
