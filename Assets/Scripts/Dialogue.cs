using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    [Header("Settings")]
    public float typingSpeed = 0.03f;

    private string fullLine;
    private bool isTyping = false;

    public string[] lines;
    private int index = 0;

    void Update()
    {
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = fullLine;
                isTyping = false;
            }
            else
            {
                if (index < lines.Length - 1)
                {
                    index++;
                    ShowLine(lines[index]);
                }
                else
                {
                    dialogueBox.SetActive(false);
                }
            }
        }
    }

    public void ShowDialogue(string[] newLines)
    {
        lines = newLines;
        index = 0;
        ShowLine(lines[index]);
    }

    public void ShowLine(string text)
    {
        dialogueBox.SetActive(true);
        fullLine = text;
        dialogueText.text = "";
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in fullLine)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
}
