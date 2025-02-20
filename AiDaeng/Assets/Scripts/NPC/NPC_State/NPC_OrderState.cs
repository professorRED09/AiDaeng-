using UnityEngine;

public class NPC_OrderState : NPC_BaseState
{
    public override void EnterState(NPC_Manager npc)
    {
        npc.OrderProduct();     //Show order bubble
        Debug.Log("Hey you, you there, I order something please");
    }

    public override void UpdateState(NPC_Manager npc)
    {
        npc.CountdownPatience();
        if (npc.isUpset)
            npc.SwitchState(npc.leaveState); 
    }

    public override void ExitState(NPC_Manager npc)
    {
        
    }
}
