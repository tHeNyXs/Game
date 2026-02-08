using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;

    public bool isDialogueActive;
    public bool freeze;
    public bool autoDialogue;
    public float autoDelay = 2f;

    private Coroutine autoDialogueCoroutine;

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject name;
    [SerializeField] private GameObject potrailBox;
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

        playerState = playerMovement.GetComponent<PlayerState>();
    }

    public void StartDialogue(DialogueSO dialogueSO)
    {
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        if (freeze) playerState.SwitchState(PlayerState.State.Dialogue);
        ShowDialogue();
        if (autoDialogue)
        {
            autoDialogueCoroutine = StartCoroutine(AutoAdvanceDialogue());
            if (nextButton != null)
            {
                nextButton.SetActive(false);
                name.SetActive(false);
                potrailBox.SetActive(false);
            }
        }
        
    }

    private IEnumerator AutoAdvanceDialogue()
    {
        while (isDialogueActive)
        {
            yield return new WaitForSeconds(autoDelay);
            AdvanceDialogue();
        }
    }

    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
        {
            ShowDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        portrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;
    }

    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (autoDialogueCoroutine != null)
        {
            StopCoroutine(autoDialogueCoroutine);
            autoDialogueCoroutine = null;
            if (nextButton != null)
            {
                nextButton.SetActive(true);
                name.SetActive(true);
                potrailBox.SetActive(true);
            }
        }
        
        if (freeze) playerState.SwitchState(PlayerState.State.Normal);
    }
}
