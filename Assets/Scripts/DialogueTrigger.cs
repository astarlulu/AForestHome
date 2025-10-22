using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager; // Drag your DialogueManager here
    public GameObject dialogueUI;            // Drag your dialogue panel here
    public Animator npcAnimator;             // Optional: NPC animation

    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueUI.SetActive(true);
        dialogueManager.StartDialogue();

        
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);

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
