using UnityEngine;
using System.Collections;
using UnityEngine;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wandering Area")]
    public float wanderWidth = 5f;
    public float wanderHeight = 5f;
    public Vector2 startingPosition;

    public float pauseDuration = 1f;
    public float speed = 2f;
    public Vector2 target;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isPaused = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim =  GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PauseAndPickNewDirection());
    }

    void Update()
    {
        if (isPaused) {
            rb.velocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(PauseAndPickNewDirection());
        }

        Move();
    }

    private void Move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;  
        if (direction.x > 0 && transform.localScale.x > 0 || direction.x < 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        rb.velocity = direction * speed;
    }

    IEnumerator PauseAndPickNewDirection()
    {
        isPaused = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(pauseDuration);

        target = GetRandomTarget();
        isPaused = false;
        anim.Play("Walk");
    }

    public void OnCollaisionEnter2D(Collision2D collision)
    {
        StartCoroutine(PauseAndPickNewDirection());
    }

    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2f;
        float halfHeight = wanderHeight / 2f;
        int edge = Random.Range(0, 4);

        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),
            1 => new Vector2(startingPosition.x + halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),
            2 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y - halfHeight),
            _ => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.y + halfHeight), startingPosition.y + halfHeight),
        };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }
}
