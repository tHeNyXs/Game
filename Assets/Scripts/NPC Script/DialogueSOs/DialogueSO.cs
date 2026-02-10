using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;
}

public enum DialogueResult
{
    None,
    Win,
    Die
}


[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3, 5)] public string text;
    public DialogueResult result;
}

[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public DialogueSO nextDialogue;

}