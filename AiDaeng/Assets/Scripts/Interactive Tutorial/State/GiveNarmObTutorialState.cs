using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveNarmObTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {
        tutorial.SetSmallNarrativeText("ลองเอาไปเสิร์ฟให้เค้าดูสิ");       
        Debug.Log("ENTER GIVE NARM OB STATE");
        //tutorial.ShowPointer(-1);        
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        tutorial.HideAllPointer();
        //  must know the state of npc
        if (tutorial.tutorialNPC.getProduct)
        {              
            tutorial.SwitchState(tutorial.onRightNarmObState);
        }
        else if (tutorial.tutorialNPC == null)
        {            
            tutorial.SwitchState(tutorial.onNPCRageLeaveState);
        }
    }  
    

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
