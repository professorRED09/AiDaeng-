using UnityEngine;

// wait to take a free order spot
public class NPC_WaitToOrderState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        Debug.Log("ENTER WAIT STATE");
        npc.currentWaypointIndex = 0;
    }

    public override void UpdateState(NPC2 npc)
    {
        // check if there's a free spot to take and order 
        npc.StartCoroutine(npc.CheckOrderSpot());

        // if order spot is taken by them, then go to that spot
        if (npc.takenOrderSpot != null)
        {
            
            Transform[] orderSpot = { npc.takenOrderSpot };
            npc.SetWaypoints(orderSpot);

            if (Vector3.Distance(npc.transform.position, orderSpot[^1].position) <= 0.1f)
            {
                Debug.Log("SNAP");
                npc.transform.position = orderSpot[^1].position;
                npc.SwitchState(npc.orderState);

            }
        }
        

    }

    public override void ExitState(NPC2 npc)
    {
        
    }
}
