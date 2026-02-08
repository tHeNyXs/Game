using UnityEngine;
using System.Collections;

public class CutsceneElementBase : MonoBehaviour
{
    public float duration;
    public CutsceneHandler cutsceneHandler { get; private set; }

    private void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    public virtual void Execute()
    {
        // Base implementation (can be empty)
    }

    protected IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(duration);
        cutsceneHandler.PlayNextElement();
    }
}
