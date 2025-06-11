using UnityEngine;

public class NPC_PreyState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isClickable = false;
        npc.currentWaypointIndex = 0; // reset index to go to next waypoints
        //Debug.Log("THANKS, I'M MA GO PREYING");
        npc.SetWaypoints(npc.wayToPrey);
    }

    public override void UpdateState(NPC2 npc)
    {
        // check if water is overflowing, if yes, disable movement until it's not
        //npc.MoveToWaypoint();
        if (Vector3.Distance(npc.transform.position, npc.wayToPrey[npc.wayToPrey.Length - 1].position) <= 0.1f) // set position 
        {
            npc.transform.position = npc.wayToPrey[npc.wayToPrey.Length - 1].position;
            npc.isMoving = false;
        }

        //ถ้าอ่างน้ำเต็ม ให้ return
        if (BuddhaBasin.instance.isFull)
        {           
            return;
        }           

        npc.Praying(); // countdown praying time
        if (npc.prayingTime <= 0)
        {
            BuddhaBasin.instance.IncreaseWater(1);  // increase water in basin
            npc.SwitchState(npc.leaveState);        // switch to leave state
        }  

        
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
