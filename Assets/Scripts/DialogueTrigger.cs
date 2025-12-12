using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    [TextArea(2, 5)]
    public string[] dialogueLines;

    private bool hasTriggered = false;   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            dialogueSystem.ShowDialogue(dialogueLines);
        }
    }
}
