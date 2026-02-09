public interface ICutsceneDialogue
{
    void StartDialogue(DialogueSO dialogueSO);
    void SetOption(bool freeze, bool autoDialogue, float autoDelay);
}
