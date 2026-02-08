using UnityEngine;
using System.Collections;

public class PauseThenTrigger : MonoBehaviour
{
    public float pauseTime = 1.5f;
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            StartCoroutine(PauseAndDoThings());
        }
    }

    IEnumerator PauseAndDoThings()
    {
        // หยุดเกม
        Time.timeScale = 0f;

        // รอแบบไม่โดน timeScale
        yield return new WaitForSecondsRealtime(pauseTime);

        // เริ่มทำสิ่งต่าง ๆ
        TriggerAll();

        // ให้เกมเดินต่อ
        Time.timeScale = 1f;
    }

    void TriggerAll()
    {
        Debug.Log("เริ่มทำทุกอย่างแล้ว");

        // ตัวอย่าง
        // dialogueManager.StartDialogue(...)
        // cutscene.Play()
        // SceneManager.LoadScene(...)
    }
}
