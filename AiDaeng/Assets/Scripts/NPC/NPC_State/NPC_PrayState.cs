using UnityEngine;

public class NPC_PrayState : NPC_BaseState
{
    float prayingTime = 2f;
    public override void EnterState(NPC2 npc)
    {
        npc.isClickable = false;
        npc.currentWaypointIndex = 0; // reset index to go to next waypoints
        npc.HideOrder();        
        npc.SetWaypoints(npc.wayToPray);

        Debug.Log("THANKS, I'M MA GO prayING");
    }

    public override void UpdateState(NPC2 npc)
    {
        
        //if(prayingTime > 0)
        //{
        //    prayingTime -= 1 * Time.deltaTime;
        //}
        //else
        //{
        //    BuddhaBasin.instance.IncreaseWater(1);  // increase water in basin
        //    npc.SwitchState(npc.leaveState);        // switch to leave state
        //}
        // check if water is overflowing, if yes, disable movement until it's not
        //npc.MoveToWaypoint();
        if (Vector3.Distance(npc.transform.position, npc.wayToPray[^1].position) <= 0.1f) // set position 
        {
            npc.transform.position = npc.wayToPray[^1].position;
            npc.isMoving = false;

            //ถ้าอ่างน้ำเต็ม ให้ return
            if (BuddhaBasin.instance.isFull)
            {
                return;
            }

            if (prayingTime > 0)
            {
                prayingTime -= 1 * Time.deltaTime;
            }
            else
            {
                BuddhaBasin.instance.IncreaseWater();  // increase water in basin
                npc.SwitchState(npc.leaveState);        // switch to leave state
            }

            //npc.Praying(); // countdown praying time
            //if (npc.prayingTime <= 0)
            //{
            //    BuddhaBasin.instance.IncreaseWater(1);  // increase water in basin
            //    npc.SwitchState(npc.leaveState);        // switch to leave state
            //}
        }

        
               

        

        
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
