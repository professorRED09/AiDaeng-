using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {
        Time.timeScale = 0;

        tutorial.InstructPlayer();
        tutorial.SetNarrativeText("Fuck you, bitch");
        Debug.Log("Enter Intro");
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorial.ChangeIntroText();
            ////switch to the next page
            //tutorial.SetNarrativeText("FUCKKKK");                   
        }
        else if (tutorial.tutorialNPC.orderSlotLists.Count > 0)
        {
            tutorial.SwitchState(tutorial.npcOrderState);
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
