using UnityEngine;

public class NextButton : MonoBehaviour
{
    public DialogueSO dialogueSO;
    public void OnNextPressed()
    {
        if (DialogueManager.Instance.autoDialogue) return;
        DialogueManager.Instance.AdvanceDialogue();
    }

}