using UnityEngine;

public class ToggleActiive : MonoBehaviour
{
    [SerializeField] DialogueTrigger triggerScript;

    [SerializeField] BoxCollider colliderToEnable;
    [SerializeField] BoxCollider interactiveParent;

    [SerializeField] string tag;

    private bool done = false;

    public void Update()
    {
        if (triggerScript.isPlayerNear && Input.GetKeyDown(KeyCode.E) && !done)
        {
            colliderToEnable.enabled = !colliderToEnable.enabled;
            done = true;

        }

    }
}
