using UnityEngine;
using System.Collections;

public class NormalCutscene : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;

    public DialogueSO dialogueSO;
    public bool instantShow;
    public bool freeze;
    public bool autoDialogue = false;
    public float autoDelay = 2f;

    private bool hasStarted = false;
    private bool cutsceneFinished = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasStarted) return;

        if (collision.CompareTag("Player"))
        {
            hasStarted = true;
            cutsceneFinished = false;
            playerAnim = collision.GetComponent<Animator>();

            
            if (playerAnim != null)
            {
                playerAnim.Play("Idle");
            }

            StartCoroutine(WaitCutsceneThenDialogue());
        }
    }

    IEnumerator WaitCutsceneThenDialogue()
    {
        while (!cutsceneFinished && !instantShow)
            yield return null;
        DialogueManager.Instance.freeze = freeze;
        DialogueManager.Instance.autoDelay = autoDelay;
        DialogueManager.Instance.autoDialogue = autoDialogue;
        DialogueManager.Instance.StartDialogue(dialogueSO);
    }

    public void OnCutsceneFinished()
    {
        cutsceneFinished = true;
    }
}
