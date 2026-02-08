using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            anim.Play("End");

        if (Input.GetKeyDown(KeyCode.Q))
            anim.Play("Start");
    }
}
