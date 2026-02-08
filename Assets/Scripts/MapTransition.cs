using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapTransition : MonoBehaviour
{

    [SerializeField] private PolygonCollider2D mapBoundary;
    private CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    enum Direction { Up, Down, Left, Right, None }
    [SerializeField] Animator transitionAnimator;
    private bool isTransitioning = false;

    [SerializeField] private float delay = 0f;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(Transition(collision.gameObject));
        }
    }

    IEnumerator Transition(GameObject player)
    {
        isTransitioning = true;

        PlayerMovement move = player.GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        transitionAnimator.Play("End");
        yield return new WaitForSeconds(1f);

        confiner.BoundingShape2D = mapBoundary;
        confiner.InvalidateCache();
        UpdatePlayerPosition(player);

        transitionAnimator.Play("Start");
        yield return new WaitForSeconds(1f);

        if (move != null) move.enabled = true;
        isTransitioning = false;
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPosition = player.transform.position;
        float offset = 7f;

        switch (direction)
        {
            case Direction.Up:
                newPosition.y += offset;
                break;
            case Direction.Down:
                newPosition.y -= offset;
                break;
            case Direction.Left:
                newPosition.x -= offset;
                break;
            case Direction.Right:
                newPosition.x += offset;
                break;
            case Direction.None:
                break;
        }

        player.transform.position = newPosition;
    }

}
