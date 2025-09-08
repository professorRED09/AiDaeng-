using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFullInventoryTutorialState : TutorialState
{
    private string[] _textArray = new string[] { "ในระหว่างที่เล่น ผู้เล่นอาจทำน้ำอบหรือของอื่นๆเพลินจนช่องเก็บของเต็ม"
                                                ,"ผู้เล่นสามารถแก้ปัญหาได้ด้วยการคลิกที่ช่องเก็บของช่องใดช่องหนึ่งเพื่อทิ้งของชิ้นนั้นๆ"};
    private int _currentIndex = 0;

    public override void EnterState(InteractiveTutorial tutorial)
    {
        tutorial.SetNarrativeText(_textArray[_currentIndex++]);
        tutorial.ShowNarrativeText(true);
        tutorial.ChangeIllust(3);
        //set inventory to full inventory
        Inventory.instance.GenerateItemsForTutorial(tutorial.completedItem);
        Debug.Log("ENTER ON FULL INVENTORY");
        
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
                tutorial.SetSmallNarrativeText("คลิกที่สล็อตช่องเก็บของเพื่อทิ้งของชิ้นนั้นๆ");
                tutorial.ShowSmallNarrativeText(true);
                tutorial.ChangeIllust(-1);                
            }
        }
        else if (Inventory.instance.items.Count <= 3)
        {            
            tutorial.SwitchState(tutorial.gameRuleState);
        }
    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
