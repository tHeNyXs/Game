using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum State
    {
        Normal,
        Cutscene,
        Dialogue
    }

    public State currentState = State.Normal;

    public void SwitchState(State newState)
    {
        currentState = newState;
    }
}
