using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOrderTutorialState : TutorialState
{
    private string[] _textArray = new string[] { "ดูของที่ผู้คนสั่งให้ดี", "จำเป็นต้องเสิร์ฟให้ทันก่อนเค้าจะอารมณ์เสีย", "ลองค่อยๆหยิบของตามที่จะบอกนะ" };
    private int _currentIndex = 0;

    public override void EnterState(InteractiveTutorial tutorial)
    {
        tutorial.ShowSmallNarrativeText(false);
        tutorial.ShowNarrativeText(true);
        tutorial.ChangeIllust(0);

        
        tutorial.SetNarrativeText(_textArray[_currentIndex++]);
        Debug.Log("ENTER NPC ORDER STATE");
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_currentIndex <= _textArray.Length - 1)
            {
                tutorial.ChangeIllust(1);
                tutorial.SetNarrativeText(_textArray[_currentIndex++]);                              
            }
            else
            {
                Time.timeScale = 1f;
                tutorial.ChangeIllust(-1);
                tutorial.SwitchState(tutorial.makeNarmObState);
            }          
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
