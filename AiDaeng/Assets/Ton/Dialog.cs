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
    public GameObject daraIMG;

    [Header("Choice")]
    public GameObject Choice_1;
    public GameObject Choice_2;
    public GameObject Choice_3;

    [TextArea(3, 10)]
    public string[] lines;
    public string[] names;
    public float textSpeed;
    public bool _isTyping = false;
    private bool _isChoice1Finished = false;
   
    #endregion

    private int index;
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
        if (index == 14 && !_isChoice1Finished)
        {
            _isChoice = true;
            Choice_1.SetActive(true);
            _isChoice1Finished = true;
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

        //soundmanager.dialogueSource.Stop();
        //soundmanager.PlayDialogue(soundmanager.dialogue);

        int maxLength = Mathf.Max(names[index].Length, lines[index].Length);
        for (int i = 0; i < maxLength; i++)
        {
            //if(i < names[index].Length)
            //{
            //    textNames.text += names[index][i];
            //}
            if (i < lines[index].Length)
            {
                textComponent.text += lines[index][i];
            }
            yield return new WaitForSeconds(textSpeed);
        }
        _isTyping = false;
        //soundmanager.dialogueSource.Stop();
    }
    void NextLine()
    {   
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            UpdateName();
            StartCoroutine(TypeLine());
            if (index == 4)
            {
                if (oldBG != null) oldBG.SetActive(false);
                if (newBG != null) newBG.SetActive(true);
            }
            if (index == 8 && daraIMG != null)
            {
                daraIMG.SetActive(true);
            }

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
