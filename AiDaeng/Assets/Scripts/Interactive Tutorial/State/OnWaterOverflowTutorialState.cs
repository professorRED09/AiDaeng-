using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWaterOverflowTutorialState : TutorialState
{
    private string[] _textArray = new string[] { "ในระหว่างที่ผู้คนสรงน้ำพระพุทธรูปไปเรื่อยๆ น้ำที่ซุ้มพระพุทธรูปก็จะค่อยๆเพิ่มขึ้น จนในที่สุดก็จะเต็ม",
                                                "ผู้เล่นต้องคอยมาสังเกตและมาเปลี่ยนภาชนะรองน้ำ เพื่อให้ผู้คนสามารถสรงน้ำต่อได้"};
    private int _currentIndex = 0;
    public override void EnterState(InteractiveTutorial tutorial)
    {
        Time.timeScale = 1f;
        Debug.Log("ENTER WATER OVERFLOW");
        tutorial.SetNarrativeText(_textArray[_currentIndex++]);
        tutorial.ShowNarrativeText(true);
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        //  must know the state of npc
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentIndex <= _textArray.Length - 1)
            {
                tutorial.SetNarrativeText(_textArray[_currentIndex++]);
            }
            else
            {
                Time.timeScale = 1f;
                tutorial.ShowNarrativeText(false);
                tutorial.SetSmallNarrativeText("คลิกที่ซุ้มพระพุทธรูปเพื่อเปลี่ยนภาชนะรองน้ำ");
                tutorial.ShowSmallNarrativeText(true);
            }
        } 
        else if (tutorial.buddhaBasin.currentWB <= 0)
        {
            tutorial.SetSmallNarrativeText("คลิกที่ซุ้มพระพุทธรูปเพื่อเปลี่ยนภาชนะรองน้ำ");
            tutorial.ShowSmallNarrativeText(false);
            //tutorial.SwitchState(tutorial.gameRuleState);
            tutorial.SwitchState(tutorial.onFullInventoryState);
        }       
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        //Show UI Box ask player if they are ready for level 1
        
    }
}
