using UnityEngine;

public class NPC_LeaveState : NPC_BaseState
{
    public override void EnterState(NPC npc)
    {
        Debug.Log("Fuck this shit, I'm out!");
    }

    public override void UpdateState(NPC npc)
    {
        
    }

    public override void ExitState(NPC npc)
    {
        
    }
}
