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
    [SerializeField] GameObject dialogueUI;            // Drag your dialogue panel here
    [SerializeField] Player player;

    private int dialogueIndex;
    private DialogueTrigger trigger;

    public void StartDialogue(DialogueTrigger ctx, List<DialogueLine> dl)
    {
        dialogueLines = dl;
        trigger = ctx;
        player.FreezePlayer(true);
        dialogueUI.SetActive(true);
        dialogueIndex = 0;
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
        dialogueUI.SetActive(false);
        player.FreezePlayer(false);
        dialogueLines.Clear();

        if (trigger != null)
        {
            trigger.EndDialogue();
        }
    }
}
