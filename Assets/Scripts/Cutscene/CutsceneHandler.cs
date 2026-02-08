using UnityEngine;
using Unity.Cinemachine;

public class CutsceneHandler : MonoBehaviour
{
    public Camera cam;
    public CinemachineCamera vCam;

    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;

    public void Start()
    {
        cutsceneElements = GetComponents<CutsceneElementBase>();
    }

    private void ExcuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Length)
        {
            cutsceneElements[index].Execute();
        }
    }

    public void PlayNextElement()
    {
        index++;
        ExcuteCurrentElement();
    }

}
