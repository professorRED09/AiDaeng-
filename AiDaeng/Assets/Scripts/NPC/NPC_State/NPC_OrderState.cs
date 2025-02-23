using UnityEngine;

public class NPC_OrderState : NPC_BaseState
{
    public override void EnterState(NPC npc)
    {
        npc.OrderProduct();     //Show order bubble
        Debug.Log("Hey you, you there, I order something please");
    }

    public override void UpdateState(NPC npc)
    {
        npc.CountdownPatience();
        if (npc.isUpset)
            npc.SwitchState(npc.leaveState);
        if (npc.getProduct)
            npc.SwitchState(npc.preyState);

        // npc get product 
    }

    public override void ExitState(NPC npc)
    {
        
    }
}
