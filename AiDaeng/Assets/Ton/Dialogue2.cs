using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image characterLeft;
    public Image characterRight;
    public Button continueButton;
    public Button skipButton;

    public string[] lines;
    public string[] names;
    public bool[] isLeftSpeaker;

    [Header("Sprites")]
    public Sprite leftIdleSprite;
    public Sprite rightIdleSprite;
    public Sprite[] leftTalkingSprites;
    public Sprite[] rightTalkingSprites;

    [Header("Settings")]
    public float textSpeed = 0.05f;
    public float talkFrameDelay = 0.1f;

    private int index = 0;
    private bool isTyping = false;

    private Coroutine leftTalkRoutine;
    private Coroutine rightTalkRoutine;

    [System.Serializable]
    public class DialogueChoice
    {
        public int lineIndex;
        public string[] choices;
        public int[] nextLineIndex;
    }

    [Header("Choices UI")]
    public GameObject choicePanel;
    public Button[] choiceButtons;
    public DialogueChoice[] dialogueChoices;

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
                StopTalkingAnimations();
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
        nameText.text = "";

        StartTalkingAnimation();

        int maxLength = Mathf.Max(names[index].Length, lines[index].Length);

        for (int i = 0; i < maxLength; i++)
        {
            if (i < names[index].Length) nameText.text += names[index][i];
            if (i < lines[index].Length) dialogueText.text += lines[index][i];
            yield return new WaitForSeconds(textSpeed);
        }

        StopTalkingAnimations();
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
            characterLeft.sprite = leftIdleSprite;
            characterRight.sprite = rightIdleSprite;
            characterLeft.color = new Color(1, 1, 1, 1);
            characterRight.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            characterLeft.sprite = leftIdleSprite;
            characterRight.sprite = rightIdleSprite;
            characterLeft.color = new Color(1, 1, 1, 0.5f);
            characterRight.color = new Color(1, 1, 1, 1);
        }
    }

    void StartTalkingAnimation()
    {
        StopTalkingAnimations(); // เผื่ออันเก่ายังเล่นอยู่

        if (isLeftSpeaker[index] && leftTalkingSprites.Length > 0)
        {
            leftTalkRoutine = StartCoroutine(AnimateSprite(characterLeft, leftTalkingSprites));
        }
        else if (!isLeftSpeaker[index] && rightTalkingSprites.Length > 0)
        {
            rightTalkRoutine = StartCoroutine(AnimateSprite(characterRight, rightTalkingSprites));
        }
    }

    void StopTalkingAnimations()
    {
        if (leftTalkRoutine != null) StopCoroutine(leftTalkRoutine);
        if (rightTalkRoutine != null) StopCoroutine(rightTalkRoutine);

        characterLeft.sprite = leftIdleSprite;
        characterRight.sprite = rightIdleSprite;
    }

    IEnumerator AnimateSprite(Image target, Sprite[] sprites)
    {
        int i = 0;
        while (true)
        {
            target.sprite = sprites[i];
            i = (i + 1) % sprites.Length;
            yield return new WaitForSeconds(talkFrameDelay);
        }
    }

    void OnSkipClicked()
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