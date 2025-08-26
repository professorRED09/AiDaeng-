using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNarmObTutorialState : TutorialState
{    
    public override void EnterState(InteractiveTutorial tutorial)
    {
        Time.timeScale = 1f;

        tutorial.SetSmallNarrativeText("เริ่มจากไปหยิบขันมาก่อน");
        tutorial.ShowSmallNarrativeText(true);
        tutorial.ShowNarrativeText(false);

        
        tutorial.player.GetComponent<PlayerController1>().enabled = true;
        Debug.Log("ENTER MAKE NARM OB STATE");

        //_taskArray = tutorial.taskTransformArray;
        tutorial.ShowPointer(0);
    }

    public override void UpdateState(InteractiveTutorial tutorial)
    {
        if (tutorial.tutorialNPC == null)
        {
            tutorial.ShowPointer(-1);
            tutorial.SwitchState(tutorial.onNPCRageLeaveState);
        }

        if (Inventory.instance.items.Count > 0)
        {
            tutorial.ShowPointer(-1);
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
                tutorial.SetSmallNarrativeText("ทีงี้เอาไปใส่น้ำ");
                tutorial.ShowPointer(1);
                //Debug.Log("ทีงี้เอาไปใส่น้ำ ไอเวร");
            }
            else if (tutorial.mixturePanel.tempItem == tutorial.processItem[1])
            {
                tutorial.SetSmallNarrativeText("ใส่น้ำอบต่อ ไป!");
                tutorial.ShowPointer(2);
                //Debug.Log("ใส่น้ำอบต่อ ไป!");
            }
            else if (tutorial.mixturePanel.tempItem == tutorial.processItem[2])
            {
                tutorial.SetSmallNarrativeText("ไปใส่ดอกไม้ \n ดูดอกไม้ดีๆล่ะ");
                tutorial.ShowPointer(3);
                //Debug.Log("ไปใส่ดอกไม้");
            }
            else
            {

            }
        }       

    }

    public override void ExitState(InteractiveTutorial tutorial)
    {
        
    }
}
