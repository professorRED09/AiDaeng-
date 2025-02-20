using UnityEngine;

public class NPC_LeaveState : NPC_BaseState
{
    public override void EnterState(NPC_Manager npc)
    {
        Debug.Log("Fuck this shit, I'm out!");
    }

    public override void UpdateState(NPC_Manager npc)
    {
        
    }

    public override void ExitState(NPC_Manager npc)
    {
        
    }
}
