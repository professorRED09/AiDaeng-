using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // ใช้ TextMeshProUGUI ถ้าใช้ UI
    public string[] lines;
    public float textSpeed = 0.05f;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        textMeshPro.text = string.Empty; // เริ่มต้นให้ Text ว่างเปล่า
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // แก้ input เป็น Input
        {
            if (isTyping)
            {
                // ข้ามไปให้ข้อความพิมพ์เสร็จทันที
                StopAllCoroutines();
                textMeshPro.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textMeshPro.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false); // แก้ fals เป็น false
        }
    }

}
