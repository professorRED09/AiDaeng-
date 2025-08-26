using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject choicePanel;
    public Button choiceButtonPrefab;

    [Header("Dialogue Data")]
    [TextArea(3, 10)]
    public string[] lines;       // ประโยคที่จะพูด
    public string[] names;       // ชื่อคนพูด ต้อง index ตรงกับ lines
    public float textSpeed = 0.05f;

    [Header("Characters")]
    public List<string> characterNames;       // ใส่ชื่อใน Inspector
    public List<Animator> characterAnimators; // Animator ของแต่ละตัวละคร

    private Dictionary<string, Animator> animatorMap;
    private int index;
    private Coroutine typingCoroutine;

    void Start()
    {
        animatorMap = new Dictionary<string, Animator>();
        for (int i = 0; i < characterNames.Count; i++)
        {
            animatorMap.Add(characterNames[i], characterAnimators[i]);
        }

        dialogueText.text = "";
        nameText.text = "";
        index = 0;

        StartDialogue();
    }

    void StartDialogue()
    {
        ShowLine();
    }

    void ShowLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        nameText.text = names[index];

        // เปิด Talk animation
        if (animatorMap.ContainsKey(names[index]))
        {
            animatorMap[names[index]].SetTrigger("Talk");
        }

        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // ปิด Talk animation
        if (animatorMap.ContainsKey(names[index]))
        {
            animatorMap[names[index]].SetTrigger("StopTalk");
        }

        // ถ้ามี Choice ที่ประโยคนี้ → แสดงปุ่ม
        if (index == 2) // 🔹 กำหนด index ที่จะมี choice
        {
            ShowChoices(new string[] { "ใช่", "ไม่ใช่", "ขอคิดดูก่อน" });
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            ShowLine();
        }
        else
        {
            dialogueText.text = "";
            nameText.text = "";
            Debug.Log("Dialogue End");
        }
    }

    void ShowChoices(string[] choices)
    {
        // ล้างปุ่มเก่า
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        choicePanel.SetActive(true);

        foreach (string choice in choices)
        {
            Button btn = Instantiate(choiceButtonPrefab, choicePanel.transform);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = choice;
            btn.onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }

    void OnChoiceSelected(string choice)
    {
        choicePanel.SetActive(false);
        Debug.Log("Player selected: " + choice);

        // คุณสามารถใส่ logic ที่นี่ เช่น เปลี่ยนเส้นทางบทสนทนา
        NextLine();
    }
}
