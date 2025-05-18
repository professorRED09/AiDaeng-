using UnityEngine;

public class NPC_ArriveState : NPC_BaseState
{
    //Vector3 npcStartWP;
    //Vector3 npcCounterWP;
    public override void EnterState(NPC2 npc)
    {
        npc.isClickable = false;
        npc.currentWaypointIndex = 0;
        //Debug.Log("I'll walk to the counter over there!");
    }

    public override void UpdateState(NPC2 npc)
    {
        //walk to counter
        if (Vector3.Distance(npc.transform.position, npc.wayToCounter[npc.wayToCounter.Length - 1].transform) <= 0.1f)
        {
            npc.transform.position = npc.wayToCounter[npc.wayToCounter.Length - 1].transform;
            // timer for thinking process
            npc.SwitchState(npc.orderState);
        }       
    }

    //public override void UpdateState(NPC npc)
    //{
    //    //walk to counter
    //    npc.WalkToCounter();
    //    if (Vector3.Distance(npc.transform.position, npc.wayToCounter[npc.wayToCounter.Length - 1].transform) <= 0.1f)
    //    {
    //        npc.transform.position = npc.wayToCounter[npc.wayToCounter.Length - 1].transform;
    //        npc.SwitchState(npc.orderState);
    //    }

    //    //if (npc.transform.position.x >= npc.counterWP.transform.position.x)
    //    //    npc.SwitchState(npc.orderState);

    //}

    public override void ExitState(NPC2 npc)
    {
        
    }
}
