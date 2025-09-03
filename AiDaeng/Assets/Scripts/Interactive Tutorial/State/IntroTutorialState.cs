using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTutorialState : TutorialState
{
    private string[] _textArray = new string[] { "ในด่านนี้ เราจะแนะนำวิธีการเล่นพื้นฐานให้กับผู้เล่น", 
                        "ผู้เล่นจะได้รับบทเป็นแดง \n ที่จะต้องคอยให้บริการน้ำอบสำหรับสรงน้ำพระพุทธรูปแก่ผู้คน",
                        "อย่างในตอนนี้ ก็มีคนมาใช้บริการซุ้มของเราแล้ว  \n มารอดูกันว่าเค้าต้องการน้ำอบแบบไหน"};
    private int _currentIndex = 0;
    public override void EnterState(InteractiveTutorial tutorial)
    {
        //tutorial.InstructPlayer();
        tutorial.ShowNarrativeText(true);
        tutorial.SetNarrativeText(_textArray[_currentIndex++]);        

        tutorial.player.GetComponent<PlayerController1>().enabled = false;
        Debug.Log("ENTER INTRO STATE");
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
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
        else if (tutorial.tutorialNPC.orderSlotLists.Count > 0)
        {
            tutorial.SwitchState(tutorial.npcOrderState);
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
