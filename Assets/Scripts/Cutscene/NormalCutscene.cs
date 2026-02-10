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
    public enum CutsceneDialogueType
    {
        Story,
        Buff,
        Question,
    }
    public CutsceneDialogueType dialogueType;
    
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
        ICutsceneDialogue manager = null;
        switch (dialogueType)
        {
            case CutsceneDialogueType.Story:
                manager = DialogueManager.Instance;
                break;

            case CutsceneDialogueType.Buff:
                manager = BuffManager.Instance;
                break;
            case CutsceneDialogueType.Question:
                manager = QuestionManager.Instance;
                break;
        }

        if (manager == null)
        {
            Debug.LogError("Dialogue Manager not found!");
            yield break;
        }

        manager.SetOption(freeze, autoDialogue, autoDelay);
        manager.StartDialogue(dialogueSO);
    }

    public void OnCutsceneFinished()
    {
        cutsceneFinished = true;
    }
}
