using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    public Dialog dialog;
    public void OnButtonNo()
    {
        //SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        // Do somethiing else
        Debug.Log("No thanks bro");
        dialog.Choice_1.SetActive(false);
        dialog.Choice_2.SetActive(false);
        dialog._isChoice = false;
        dialog.isChoice1Finished = false;
    }
    public void OnButtonYesFirst() 
    {
        Debug.Log("Yes Daddy");
        dialog.Choice_1.SetActive(false);
        dialog.Choice_2.SetActive(false);
        dialog._isChoice = false;
        dialog.isChoice1Finished = false;
        // Do somethiing else

        //SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        //GameDataHandler.instance.ClearData();
    }
    public void OnButtonYesSecond()
    {
        


        //SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
    }
}
