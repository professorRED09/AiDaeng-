using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveNarmObTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {        
        tutorial.SetSmallNarrativeText("ลองเอาไปเสิร์ฟให้เค้าดูสิ");
        //tutorial.ShowPointer(-1);        
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        //  must know the state of npc
        if(tutorial.tutorialNPC.getProduct)
        {              
            tutorial.SwitchState(tutorial.onRightNarmObState);
        }
        else if (tutorial.tutorialNPC == null)
        {
            tutorial.ShowPointer(-1);
            tutorial.SwitchState(tutorial.onNPCRageLeaveState);
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
