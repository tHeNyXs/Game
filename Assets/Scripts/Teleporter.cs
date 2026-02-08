using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportTarget;
    public Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.position = teleportTarget.position;
        }
    }
}
