using UnityEngine;

public abstract class NPC_BaseState 
{
    public abstract void EnterState(NPC2 npc);
    public abstract void UpdateState(NPC2 npc);
    public abstract void ExitState(NPC2 npc);
}
