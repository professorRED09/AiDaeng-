using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveNarmObTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {
        tutorial.LetPlayerPlay();
        Debug.Log("Tell player to try giving npc the product");
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        //  must know the state of npc
        if(tutorial.tutorialNPC.getProduct)
        {
            ExitState(tutorial);
            tutorial.SwitchState(tutorial.introState);
        } 
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
