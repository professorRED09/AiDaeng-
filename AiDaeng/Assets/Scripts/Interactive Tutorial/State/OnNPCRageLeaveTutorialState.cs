using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNPCRageLeaveTutorialState : TutorialState
{
    //private string[] _textArray = new string[] { "จำเป็นต้องเสิร์ฟให้ทันก่อนเค้าจะอารมณ์เสีย", "นะจ๊ะ เอ้า ลุย" };
    //private int _currentIndex = 0;
    public override void EnterState(InteractiveTutorial tutorial)
    {
        tutorial.HideAllPointer();
        tutorial.SetNarrativeText("เมื่อผู้คนอารมณ์เสียและเดินออกจากฉาก คุณจะพลาดคะแนนที่จะได้จากคนคนนั้น \n หากพลาดมากๆ อาจทำให้ไม่ผ่านด่านได้");
        Time.timeScale = 1f;
        Debug.Log("ENTER NPC RAGE LEAVE STATE");        
        tutorial.ShowNarrativeText(true);
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            tutorial.ShowUIBox(tutorial.TryAgainUIBox);
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        //Show UI Box ask player if they are ready for level 1
        
    }
}
