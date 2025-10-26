using System.Collections.Generic;
using UnityEngine;
using TMPro; // only if using TextMeshPro

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager; // Drag your DialogueManager here
    public Animator npcAnimator;             // Optional: NPC animation
    [SerializeField] private List<DialogueLine> dialogueLines;

    [Header("UI Prompt")]
    public GameObject interactUIPrompt; // Drag your "Press E to talk" UI here

    public bool isPlayerNear = false;

    private bool dialogueActive = false;

    void Update()
    {
        if (isPlayerNear && !dialogueActive)
        {
            // Show UI prompt when near
            if (interactUIPrompt != null && !interactUIPrompt.activeSelf)
                interactUIPrompt.SetActive(true);

            // Start dialogue on E press
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
                interactUIPrompt.SetActive(false); // Hide prompt while talking
            }
        }
        else
        {
            // Hide UI when player leaves range
            if (interactUIPrompt != null && interactUIPrompt.activeSelf)
                interactUIPrompt.SetActive(false);
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        dialogueManager.StartDialogue(this, dialogueLines);

        if (npcAnimator != null)
            npcAnimator.SetTrigger("Talk");
    }

    public void EndDialogue()
    {
        dialogueActive = false;

        if (npcAnimator != null)
            npcAnimator.SetTrigger("Idle");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactUIPrompt != null)
                interactUIPrompt.SetActive(false);
        }
    }
}
