using UnityEngine;

public class NPC_RageLeaveState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isMoving = true; 
        npc.isClickable = false;    // player unable to click on the npc
        npc.HideOrder();            // hide bubble ui of npc
        //npc.currentWaypointIndex = 0;   // reset index
        //npc.SetWaypoints(npc.wayToRageLeave);   //make their way to leave with rage        
    }

    public override void UpdateState(NPC2 npc)
    {
        npc.RageMood();
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
