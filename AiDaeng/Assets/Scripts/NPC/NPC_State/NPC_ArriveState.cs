using UnityEngine;

public class NPC_ArriveState : NPC_BaseState
{ 
    public override void EnterState(NPC2 npc)
    {
        npc.isClickable = false;
        npc.currentWaypointIndex = 0; // reset index to go to next waypoints
        //Debug.Log("I'll walk to the counter over there!");
    }

    public override void UpdateState(NPC2 npc)
    {
        if (npc.isUpset)
        {
            npc.SwitchState(npc.rageLeaveState);
        }
        //npc.MoveToWaypoint(); // walk to counter
        if (Vector3.Distance(npc.transform.position, npc.wayToCounter[npc.wayToCounter.Length - 1].position) <= 0.1f)
        {
            npc.transform.position = npc.wayToCounter[npc.wayToCounter.Length - 1].position;
            //npc.SwitchState(npc.orderState);
            npc.SwitchState(npc.waitToOrderState);

        }       
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
