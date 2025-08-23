using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNarmObTutorialState : TutorialState
{
    public override void EnterState(InteractiveTutorial tutorial)
    {
        Time.timeScale = 1f;
        tutorial.LetPlayerPlay();
        Debug.Log("Now, let's make narm ob");
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (Inventory.instance.items.Count > 0)
        {
            tutorial.SwitchState(tutorial.giveNarmObState);
        }

        if (tutorial.mixturePanel.tempItem == null)
        {
            return;
        }
        else
        {
            if (tutorial.mixturePanel.tempItem == tutorial.processItem[0])
            {
                tutorial.SetSmallNarrativeText("- ทีงี้เอาไปใส่น้ำ ไอเวร -");
                //Debug.Log("ทีงี้เอาไปใส่น้ำ ไอเวร");
            }
            else if (tutorial.mixturePanel.tempItem == tutorial.processItem[1])
            {
                tutorial.SetSmallNarrativeText("- ใส่น้ำอบต่อ ไป! -");
                //Debug.Log("ใส่น้ำอบต่อ ไป!");
            }
            else if (tutorial.mixturePanel.tempItem == tutorial.processItem[2])
            {
                tutorial.SetSmallNarrativeText("- ไปใส่ดอกไม้ -");
                //Debug.Log("ไปใส่ดอกไม้");
            }
        }       

    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
