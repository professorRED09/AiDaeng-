using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleDialogue : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image characterLeft;
    public Image characterRight;
    public Button continueButton;
    public Button skipButton;

    [Header("Dialogue Data")]
    public string[] lines;
    public string[] names;
    public bool[] isLeftSpeaker;

    [Header("Animators")]
    public Animator leftAnimator;
    public Animator rightAnimator;

    [Header("Settings")]
    public float textSpeed = 0.05f;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        dialogueText.text = "";
        nameText.text = "";
        continueButton.gameObject.SetActive(false);
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
                StopTalking();

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
        if (lines.Length == 0 || names.Length == 0 || isLeftSpeaker.Length == 0)
        {
            Debug.LogError("Missing dialogue data.");
            return;
        }
        UpdateUI();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        nameText.text = names[index]; // โชว์ชื่อทันที

        StartTalking();

        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        StopTalking();
        isTyping = false;
        continueButton.gameObject.SetActive(true);
    }

    void NextLine()
    {
        continueButton.gameObject.SetActive(false);
        if (index < lines.Length - 1)
        {
            index++;
            UpdateUI();
            StartCoroutine(TypeLine());
        }
        else
        {
            SceneManager.LoadScene("Gameplay [Beta]");
        }
    }

    void UpdateUI()
    {
        if (isLeftSpeaker[index])
        {
            characterLeft.color = new Color(1, 1, 1, 1);
            characterRight.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            characterLeft.color = new Color(1, 1, 1, 0.5f);
            characterRight.color = new Color(1, 1, 1, 1);
        }
    }

    void StartTalking()
    {
        if (isLeftSpeaker[index])
        {
            leftAnimator.SetBool("isTalking", true);
        }
        else
        {
            rightAnimator.SetBool("isTalking", true);
        }
    }

    void StopTalking()
    {
        if (isLeftSpeaker[index])
        {
            leftAnimator.SetBool("isTalking", false);
        }
        else
        {
            rightAnimator.SetBool("isTalking", false);
        }
    }

    void OnSkipClicked()
    {
        StartCoroutine(SkipToEnd());
    }

    IEnumerator SkipToEnd()
    {
        StopAllCoroutines();
        index = lines.Length - 1;
        UpdateUI();

        dialogueText.text = lines[index];
        nameText.text = names[index];
        isTyping = false;
        continueButton.gameObject.SetActive(true);

        StopTalking();

        yield return new WaitForSeconds(1f); // โชว์บรรทัดสุดท้ายก่อนเปลี่ยน Scene
        SceneManager.LoadScene("Gameplay [Beta]");
    }
}