using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName;
        public Sprite characterSprite;
        [TextArea] public string dialogueText;
    }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    public Animator characterAnimator; // Animator ที่คุมปากพูด
    public float typingSpeed = 0.05f;

    public DialogueLine[] lines;
    private int index = 0;

    public void StartDialogue()
    {
        index = 0;
        ShowLine();
    }

    public void ShowLine()
    {
        if (index < lines.Length)
        {
            nameText.text = lines[index].characterName;
            characterImage.sprite = lines[index].characterSprite;
            StopAllCoroutines();
            StartCoroutine(TypeLine(lines[index].dialogueText));
        }
        else
        {
            dialogueText.text = "";
            nameText.text = "";
        }
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        characterAnimator.SetBool("isTalking", true); // เริ่มพูด

        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        characterAnimator.SetBool("isTalking", false); // จบการพูด
        index++;
    }

    public void NextLine()
    {
        ShowLine();
    }
}
