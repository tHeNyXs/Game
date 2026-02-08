using UnityEngine;

public class NPC_TriggerCall : MonoBehaviour
{
    public NPC_Patrol npc;
    public Transform callPoint;
    [SerializeField] public bool notifyCutsceneFinished = false;
    [SerializeField] private NormalCutscene normalCutscene;

    [SerializeField] private PlayerMovement playerMovement;
    private PlayerState playerState;
    private bool hasTriggered = false;

    void Awake()
    {
        playerState = playerMovement.GetComponent<PlayerState>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;

        playerState.SwitchState(PlayerState.State.Cutscene);
        npc.notifyCutsceneFinished = notifyCutsceneFinished;
        npc.MoveAndStop(callPoint.position);
    }
}