using UnityEngine;

public class NPC_OrderState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isMoving = false;
        npc.isClickable = true;
        npc.isWaiting = false;  
        npc.ThinkOfOrder();     //Show bubble ui
        //Debug.Log("ENTER ORDER STATE");
    }

    public override void UpdateState(NPC2 npc)
    {        
        if (npc.isUpset)
        {
            npc.SwitchState(npc.rageLeaveState);
            npc.takenOrderSpot.GetComponent<OrderSpot>().isFree = true;
        }
        else if (npc.getProduct)
        {
            npc.SwitchState(npc.preyState);
            npc.takenOrderSpot.GetComponent<OrderSpot>().isFree = true;
        }
            
        
            
        // npc get product 
    }

    public override void ExitState(NPC2 npc)
    {
        // free spot = true
    }
}
