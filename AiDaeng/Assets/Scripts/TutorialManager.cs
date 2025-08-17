using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int _popUpsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < popUps.Length; i++)
        {
            if (i == _popUpsIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }       

        if(_popUpsIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _popUpsIndex++;
            }
        }
    }

    public void NextPopUpsButton()
    {
        _popUpsIndex++;
    }
}
