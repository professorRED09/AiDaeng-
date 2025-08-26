using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRightNarmObTutorialState : TutorialState
{
    private string[] _textArray = new string[] { "ยอดเยี่ยม! นายเจ๋งที่สุดเลย!",
                                                "สังเกตว่าหลังจากมอบน้ำอบได้สำเร็จ ผู้เล่นจะได้แต้มคะแนนเพิ่มขึ้นมา"};
    private int _currentIndex = 0;

    public override void EnterState(InteractiveTutorial tutorial)
    {
        tutorial.player.GetComponent<PlayerController1>().enabled = true;

        tutorial.SetNarrativeText(_textArray[_currentIndex++]);

        tutorial.ShowNarrativeText(true);
        tutorial.ShowSmallNarrativeText(false);

        Debug.Log("ENTER ON WRONG");
        
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        //  must know the state of npc
        if(Input.GetMouseButtonDown(0))
        {
            if (_currentIndex <= _textArray.Length - 1)
            {
                tutorial.SetNarrativeText(_textArray[_currentIndex++]);
            }
            else
            {
                Time.timeScale = 1f;
                tutorial.ShowNarrativeText(false);
            }            
        }
        else if (tutorial.tutorialNPC == null)
        {
            tutorial.ShowPointer(-1);
            tutorial.SwitchState(tutorial.gameRuleState);
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
