using UnityEngine;

// wait to take a free order spot
public class NPC_WaitToOrderState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {        
        npc.currentWaypointIndex = 0; //reset waypoint index 
    }

    public override void UpdateState(NPC2 npc)
    {
        //Fade if Namwan is behind
        npc.DoFade();

        // check if there's a free spot to take and order 
        npc.StartCoroutine(npc.CheckOrderSpot());

        // if order spot is taken by them, then go to that spot
        if (npc.takenOrderSpot != null)
        {

            Transform[] orderSpot = { npc.takenOrderSpot };
            npc.SetWaypoints(orderSpot);
            //npc.MoveToWaypoint();
            if (Vector3.Distance(npc.transform.position, orderSpot[^1].position) <= 0.1f)
            {
                //Debug.Log("SNAP");
                npc.transform.position = orderSpot[^1].position;
                npc.SwitchState(npc.orderState);

            }
        }
        if (npc.isUpset)
        {
            if(npc.takenOrderSpot != null)
            {
                npc.takenOrderSpot.GetComponent<OrderSpot>().isFree = true;
            }            
            npc.SwitchState(npc.rageLeaveState);
        }
    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
