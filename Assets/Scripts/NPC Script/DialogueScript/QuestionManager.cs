using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuestionManager : MonoBehaviour, ICutsceneDialogue
{
    public static QuestionManager Instance;

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public TMP_Text dialogueText;

    private DialogueSO currentDialogue;
    private int dialogueIndex;
    public Button[] choiceButtons;

    [SerializeField] private PlayerMovement playerMovement;
    private PlayerState playerState;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
            button.gameObject.SetActive(false);

        playerState = playerMovement.GetComponent<PlayerState>();
    }

     public void SetOption(bool tempFreeze, bool tempAutoDialogue, float tempAutoDelay)
    {
    }

    public void StartDialogue(DialogueSO dialogueSO)
    {
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        playerState.SwitchState(PlayerState.State.Dialogue);
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
        {
            ShowDialogue();
        }
        else
        {
            ShowChoices();
        }
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;

        if (dialogueIndex >= currentDialogue.lines.Length)
        {
            AdvanceDialogue();
        }
    }

    private void ShowChoices()
    {   
        ClearChoices();
        if (currentDialogue.options.Length > 0)
        {
            for (int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];

                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);

                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));
            }
        }
        else
        {
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "ต่อไป";
            choiceButtons[0].onClick.AddListener(EndDialogue);
            choiceButtons[0].gameObject.SetActive(true);
        }
    }

    private void ChooseOption(DialogueSO dialogueSO)
    {
        if (dialogueSO == null)
            EndDialogue();
        else
        {
            ClearChoices();
            StartDialogue(dialogueSO);
        }
    }

    private void EndDialogue()
    {
        dialogueIndex = 0;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        playerState.SwitchState(PlayerState.State.Normal);
    }

    private void ClearChoices()
    {
        foreach (var button in choiceButtons) 
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
