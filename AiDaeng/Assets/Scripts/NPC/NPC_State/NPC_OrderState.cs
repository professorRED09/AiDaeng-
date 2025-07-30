using UnityEngine;

public class NPC_OrderState : NPC_BaseState
{
    public override void EnterState(NPC2 npc)
    {
        npc.isMoving = false;        
        npc.isWaiting = false;  
        npc.ThinkOfOrder();     //Show bubble ui
        //Debug.Log("ENTER ORDER STATE");
    }

    public override void UpdateState(NPC2 npc)
    {        
        if (npc.isUpset)
        {
            npc.takenOrderSpot.GetComponent<OrderSpot>().isFree = true;
            npc.SwitchState(npc.rageLeaveState);
            
        }
        else if (npc.getProduct)
        {
            //delay time before swtiching to prayState
            npc.PlayGetItemAnimation();
            //npc.OnGettingProduct();


            //npc.takenOrderSpot.GetComponent<OrderSpot>().isFree = true;
            //npc.SwitchState(npc.prayState);

            //npc.StartCoroutine(npc.OnGettingProduct(1f));

        }
            
        
            
        // npc get product 
    }
    public override void ExitState(NPC2 npc)
    {
        
        //npc.SwitchState(npc.prayState);
    }
}
