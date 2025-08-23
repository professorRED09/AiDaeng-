using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOrderTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {
        Time.timeScale = 0;

        tutorial.InstructPlayer();
        tutorial.SetNarrativeText("- ดูของที่ผู้คนสั่งให้ดี -");
        Debug.Log("Enter Intro");
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorial.SwitchState(tutorial.makeNarmObState);
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
