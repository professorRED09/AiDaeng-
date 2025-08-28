using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System.Diagnostics;

public class Dialog : MonoBehaviour
{
    #region Variable
    [Header("Variable")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textNames;
    public TextMeshProUGUI textContinue;
    public Image imageContinue;
    public bool _isChoice = false;

    [Header("Image")]
    public GameObject oldBG;
    public GameObject newBG;
    //public GameObject NpcIMG;

    [Header("Choice")]
    public GameObject Choice_1;
    public GameObject Choice_2;

    [TextArea(3, 10)]
    public string[] lines;
    public string[] names;
    public float textSpeed;
    
    public bool _isTyping = false;
    public bool isChoice1Finished = false;

    public Animator monkAnimation;
    public Animator playerAnimation;
   
    #endregion

    private int index;
    public bool[] isQuestion;

    private void Start()
    {
        textComponent.text = string.Empty;
        textNames.text = string.Empty;
        _isChoice = false;

        StartDialog();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index] && textNames.text == names[index] && _isChoice == false)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                textNames.text = names[index];
                _isTyping = false;
            }
        }

        if (textComponent.text == lines[index])
        {
            //textContinue.gameObject.SetActive(true);
            imageContinue.gameObject.SetActive(true);
        }
        else
        {
            //textContinue.gameObject.SetActive(false);
            imageContinue.gameObject.SetActive(false);
        }

        if (index == 2 && !isChoice1Finished)
        {
            _isChoice = true;
            Choice_1.SetActive(true);
            isChoice1Finished = true;
        }
    }

    void StartDialog()
    {
        index = 0;
        UpdateName();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        textNames.text = string.Empty;
        textNames.text = names[index];

        _isTyping = true;

        int maxLength = Mathf.Max(names[index].Length, lines[index].Length);
        for (int i = 0; i < maxLength; i++)
        {
            if (i < lines[index].Length)
            {
                textComponent.text += lines[index][i];
            }
            yield return new WaitForSeconds(textSpeed);
        }
        _isTyping = false;
    }

    void NextLine()
    {   
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            UpdateName();
            StartCoroutine(TypeLine());
        }
    }

    void UpdateName()
    {
        if (index < names.Length)
        {
            textNames.text = names[index];
        }
        else
        {
            textNames.text = string.Empty;
        }
    }
}
