using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // ข้อความสนทนา
    public TextMeshProUGUI nameText;      // ชื่อผู้พูด
    public Image characterLeft;           // ตัวละครฝั่งซ้าย (ผู้เล่น)
    public Image characterRight;          // ตัวละครฝั่งขวา (NPC)

    public string[] lines;                // บทสนทนา
    public string[] names;                // ชื่อของตัวละครแต่ละบรรทัด
    public Sprite[] leftSprites;          // รูปตัวละครผู้เล่น
    public Sprite[] rightSprites;         // รูปตัวละคร NPC
    public bool[] isLeftSpeaker;          // true = ผู้เล่นพูด, false = NPC พูด

    private int index = 0;
    private bool isTyping = false;
    private float textSpeed = 0.05f;

    void Start()
    {
        dialogueText.text = "";
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // กดคลิกเพื่อข้ามหรือไปบรรทัดถัดไป
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
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
        UpdateUI();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            UpdateUI();
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false); // ปิดกล่องข้อความเมื่อจบ
        }
    }

    void UpdateUI()
    {
        nameText.text = names[index];                      // อัปเดตชื่อ
        if (isLeftSpeaker[index])
        {
            characterLeft.sprite = leftSprites[index];    // อัปเดตรูปฝั่งซ้าย
            characterRight.color = new Color(1, 1, 1, 0.5f); // ลดความโปร่งใส NPC
            characterLeft.color = new Color(1, 1, 1, 1);  // ทำให้ตัวผู้เล่นชัด
        }
        else
        {
            characterRight.sprite = rightSprites[index];  // อัปเดตรูปฝั่งขวา
            characterLeft.color = new Color(1, 1, 1, 0.5f); // ลดความโปร่งใสผู้เล่น
            characterRight.color = new Color(1, 1, 1, 1); // ทำให้ NPC ชัด
        }
    }
}

