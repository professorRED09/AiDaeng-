using UnityEngine;

public class NPC_ArriveState : NPC_BaseState
{
    //Vector3 npcStartWP;
    //Vector3 npcCounterWP;
    public override void EnterState(NPC_Manager npc)
    {
        //npcStartWP = npc.transform.position;
        //npcCounterWP = npc.counterWP.transform.position;
        Debug.Log("I'll walk to the counter over there!");
    }

    public override void UpdateState(NPC_Manager npc)
    {
        //walk to counter
        npc.WalkToCounter();
        if (Vector3.Distance(npc.transform.position, npc.counterWP.transform.position) <= 0.1f)
        {
            npc.transform.position = npc.counterWP.transform.position;
            npc.SwitchState(npc.orderState);
        }
            
        //if (npc.transform.position.x >= npc.counterWP.transform.position.x)
        //    npc.SwitchState(npc.orderState);
        
    }

    public override void ExitState(NPC_Manager npc)
    {
        
    }
}
