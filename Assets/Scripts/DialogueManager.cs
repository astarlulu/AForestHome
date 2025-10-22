using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea] public string dialogueText;
    public Sprite characterIcon;
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image characterImage;
    [SerializeField] private List<DialogueLine> dialogueLines;

    private int dialogueIndex;
    private DialogueTrigger trigger;

    public void StartDialogue()
    {
        dialogueIndex = 0;
        trigger = FindObjectOfType<DialogueTrigger>();
        ShowDialogue(dialogueIndex);
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueLines.Count)
        {
            ShowDialogue(dialogueIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowDialogue(int index)
    {
        DialogueLine line = dialogueLines[index];
        dialogueText.text = line.dialogueText;
        nameText.text = line.characterName;
        characterImage.sprite = line.characterIcon;
    }

    private void EndDialogue()
    {
        if (trigger != null)
        {
            trigger.EndDialogue();
        }
    }
}
