using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CSE_CameraZoom : CutsceneElementBase
{
    [SerializeField] private float targetFOV;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float holdTime = 0.5f;

    [SerializeField] private PlayerMovement playerMovement;
    private PlayerState playerState;

    private CinemachineCamera vCam;

    [Header("After Cutscene")]
    [SerializeField] private bool notifyCutsceneFinished = false;
    [SerializeField] private NormalCutscene normalCutscene;

    void Awake()
    {
        playerState = playerMovement.GetComponent<PlayerState>();
    }

    public override void Execute()
    {
        vCam = cutsceneHandler.vCam;
        vCam.Follow = target;
        playerState.SwitchState(PlayerState.State.Cutscene);
        StartCoroutine(ZoomCamera());
    }

    IEnumerator ZoomCamera()
    {
        Vector3 originalPosition = vCam.transform.position;
        Vector3 targetPosition = target.position + offset;

        float originalFOV = vCam.Lens.FieldOfView;
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            vCam.Lens.FieldOfView =
                Mathf.Lerp(originalFOV, targetFOV, t);

            vCam.transform.position =
                Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        vCam.Lens.FieldOfView = targetFOV;
        vCam.transform.position = targetPosition;

        if (holdTime > 0f)
            yield return new WaitForSeconds(holdTime);
        
        if (notifyCutsceneFinished && normalCutscene != null)
        {
            normalCutscene.OnCutsceneFinished();
            playerState.SwitchState(PlayerState.State.Normal);
        }
        if (normalCutscene == null)
        {
            playerState.SwitchState(PlayerState.State.Normal);
        }
        cutsceneHandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}