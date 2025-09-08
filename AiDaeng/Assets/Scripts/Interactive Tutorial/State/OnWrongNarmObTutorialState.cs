using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWrongNarmObTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {
        tutorial.HideAllPointer();
        tutorial.SetNarrativeText("เหมือนจะใส่ดอกไม้ผิดดอกนะ ลองดูสิ่งที่เค้าสั่งแล้วทำใหม่อีกครั้งดู");
        tutorial.ShowNarrativeText(true);
        Debug.Log("ENTER ON WRONG");
        
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        //  must know the state of npc
        if(Input.GetMouseButtonDown(0))
        {
            //clear item in player's inventory
            Inventory.instance.items.Clear();
            Inventory.instance.DisplayItems();

            tutorial.SwitchState(tutorial.makeNarmObState);
        } 
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
