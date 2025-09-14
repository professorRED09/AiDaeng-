using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleTutorialState : TutorialState
{
    private string[] _textArray = new string[] { "ในการผ่านด่านๆหนึ่ง จำเป็นต้องเก็บแต้มคะแนนให้ได้อย่างน้อย 1 ดาว", "ถ้าสงสัยอะไร ลองกดปุ่มนี้ตรงด้านบนดูนะ", "ตั้งใจทำงานนะ"  };
    private int _currentIndex = 0;

    public override void EnterState(InteractiveTutorial tutorial)
    {
        Time.timeScale = 1f;
        Debug.Log("ENTER GAME RULE STATE");
        tutorial.SetNarrativeText(_textArray[_currentIndex++]);
        tutorial.ShowNarrativeText(true);
        tutorial.ChangeIllust(2);        
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentIndex <= _textArray.Length - 1)
            {
                tutorial.ChangeIllust(4);
                tutorial.SetNarrativeText(_textArray[_currentIndex++]);
            }
            else
            {
                Time.timeScale = 1f;
                tutorial.ChangeIllust(-1);
                tutorial.ShowUIBox(tutorial.PlayAgainUIBox);
                Debug.Log("Show BOX");
            }

            //Time.timeScale = 1f;
            //tutorial.ChangeIllust(-1);
            //tutorial.ShowUIBox(tutorial.PlayAgainUIBox);
            //Debug.Log("Show BOX");            
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        //Show UI Box ask player if they are ready for level 1
        
    }
}
