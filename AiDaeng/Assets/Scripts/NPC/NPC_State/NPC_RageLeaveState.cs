using UnityEngine;

public class NPC_RageLeaveState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isMoving = true;        
        npc.isClickable = false;    // player unable to click on the npc
        npc.HideOrder();            // hide bubble ui of npc
        npc.isUpset = true;
        // change this npc's layer to default so the other won't stop when detect this furious npc
        npc.npcMask = LayerMask.GetMask("Default");
        // translate npc above an inch so it won't overlap with the remaining npcs which are in the queue
        npc.transform.Translate(Vector3.up * 0.1f);
        //npc.transform.position += new Vector3(0, 0.1f, 0);

        //npc.currentWaypointIndex = 0;   // reset index
        //npc.SetWaypoints(npc.wayToRageLeave);   //make their way to leave with rage        
    }

    public override void UpdateState(NPC2 npc)
    {
        npc.OnRageMood();        
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
