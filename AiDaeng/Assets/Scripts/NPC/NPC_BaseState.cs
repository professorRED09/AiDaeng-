using UnityEngine;

public abstract class NPC_BaseState 
{
    public abstract void EnterState(NPC_Manager npc);
    public abstract void UpdateState(NPC_Manager npc);
    public abstract void ExitState(NPC_Manager npc);
}
