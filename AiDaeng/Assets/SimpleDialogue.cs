using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // ข้อความสนทนา
    public TextMeshProUGUI nameText;      // ชื่อผู้พูด
    public Image characterLeft;           // ตัวละครฝั่งซ้าย (ผู้เล่น)
    public Image characterRight;          // ตัวละครฝั่งขวา (NPC)
    public Button continueButton;         // ปุ่มกดเพื่อไปต่อ
    public Button skipButton;             // ปุ่มกดเพื่อข้ามข้อความทั้งหมด

    public string[] lines;                // บทสนทนา
    public string[] names;                // ชื่อของตัวละครแต่ละบรรทัด
    public Sprite[] leftSprites;          // รูปตัวละครผู้เล่น
    public Sprite[] rightSprites;         // รูปตัวละคร NPC
    public bool[] isLeftSpeaker;          // true = ผู้เล่นพูด, false = NPC พูด

    private int index = 0;
    private bool isTyping = false;
    private float textSpeed = 0.05f;

    public AudioSource audioSource;
    public AudioClip typingSound;
    public AudioClip clickSound;
    public AudioClip skipSound;
    void Start()
    {
        dialogueText.text = "";
        nameText.text = "";
        continueButton.gameObject.SetActive(false); // ซ่อนปุ่มตอนเริ่มต้น
        //continueButton.onClick.AddListener(OnContinueClicked);
        skipButton.onClick.AddListener(OnSkipClicked);
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
                nameText.text = names[index];
                isTyping = false;
                continueButton.gameObject.SetActive(true);
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
        nameText.text = "";

        int maxLength = Mathf.Max(names[index].Length, lines[index].Length);

        for (int i = 0; i < maxLength; i++)
        {
            if (i < names[index].Length)
                nameText.text += names[index][i];

            if (i < lines[index].Length)
                dialogueText.text += lines[index][i];

            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        continueButton.gameObject.SetActive(true); // แสดงปุ่มเมื่อพิมพ์เสร็จ
    }

    void NextLine()
    {
        continueButton.gameObject.SetActive(false); // ซ่อนปุ่มตอนเปลี่ยนไดอะล็อก
        if (index < lines.Length - 1)
        {
            index++;
            UpdateUI();
            StartCoroutine(TypeLine());
        }
        else
        {
            SceneManager.LoadScene("Gameplay [Beta]");
            //gameObject.SetActive(false);
        }
    }

    void UpdateUI()
    {
        if (isLeftSpeaker[index])
        {
            characterLeft.sprite = leftSprites[index];    
            characterRight.color = new Color(1, 1, 1, 0.5f); 
            characterLeft.color = new Color(1, 1, 1, 1);  
        }
        else
        {
            characterRight.sprite = rightSprites[index];  
            characterLeft.color = new Color(1, 1, 1, 0.5f); 
            characterRight.color = new Color(1, 1, 1, 1); 
        }
    }

    void OnContinueClicked()
    {
        if (!isTyping)
        {
            NextLine();
        }
    }

    public void OnSkipClicked()
    {
        StopAllCoroutines();  
        index = lines.Length - 1;  
        UpdateUI();
        dialogueText.text = lines[index];  
        nameText.text = names[index];   
        isTyping = false;
        continueButton.gameObject.SetActive(true);
        SceneManager.LoadScene("Gameplay [Beta]");
    }
    
}

