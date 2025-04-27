using UnityEngine;

public class NPC_OrderState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isClickable = true;
        npc.OrderProduct();     //Show order bubble
        Debug.Log("Hey you, you there, I order something please");
    }

    public override void UpdateState(NPC2 npc)
    {
        npc.CountdownPatience();
        if (npc.isUpset)
            npc.SwitchState(npc.rageLeaveState);
        if (npc.getProduct)
            npc.SwitchState(npc.preyState);
        // npc get product 
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
