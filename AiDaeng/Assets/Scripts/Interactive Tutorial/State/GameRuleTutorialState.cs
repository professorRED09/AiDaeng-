using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {
        Time.timeScale = 1f;
        Debug.Log("ENTER GAME RULE STATE");
        tutorial.SetNarrativeText("ทีงี้ พี่ก็ต้องเก็บแต้มให้ครบอย่างน้อยดาวนึง เข้าใจ๊?");
        //if (tutorial.scoreManager.currentScore <= 0)
        //{
        //    tutorial.SetNarrativeText("มื่อผู้คนอารมณ์เสียและเดินออกจากฉาก \n คุณจะพลาดคะแนนที่จะได้จากคนคนนั้น หากพลาดมากๆ อาจทำให้ไม่ผ่านด่านได้");
        //}
        //else
        //{
        //    tutorial.SetNarrativeText("ทีงี้ พี่ก็ต้องเก็บแต้มให้ครบอย่างน้อยดาวนึง เข้าใจ๊?");
        //}
        tutorial.ShowNarrativeText(true);
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            tutorial.ShowUIBox(tutorial.PlayAgainUIBox);
            Debug.Log("Show BOX");            
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        //Show UI Box ask player if they are ready for level 1
        
    }
}
