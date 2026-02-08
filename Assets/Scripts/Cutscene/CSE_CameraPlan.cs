using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CSE_CameraPlan : CutsceneElementBase
{
    private CinemachineCamera vCam;
    [SerializeField] private Vector2 distanceToMove;

    public override void Execute()
    {
        vCam = cutsceneHandler.vCam;
        vCam.Follow = null;
        StartCoroutine(PanCoroutine());
    }

    private IEnumerator PanCoroutine()
    {
        Vector3 originalPosition = vCam.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(distanceToMove.x, distanceToMove.y, 0);

        float startTime = Time.time;
        float elpasedTime = 0f;

        while (elpasedTime < duration)
        {
            float t = elpasedTime / duration;
            vCam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            elpasedTime = Time.time - startTime;
            yield return null;
        }

        vCam.transform.position = targetPosition;
        cutsceneHandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}