using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager; // Drag your DialogueManager here
    public Animator npcAnimator;             // Optional: NPC animation
    [SerializeField]private List<DialogueLine> dialogueLines;
    public bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    

    void StartDialogue()
    {

        dialogueManager.StartDialogue(this, dialogueLines);
        
    }

    public void EndDialogue()
    {

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
            isPlayerNear = false;
    }
}
