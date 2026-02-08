using System.Collections;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public Vector2[] patrolPoints;
    private int currentPatrolIndex;
    public Vector2 target;

    public float pauseDuration = 1.5f;
    private bool isPaused = false;
    private bool isLocked = false;
    public NormalCutscene normalCutscene;

    public float speed = 2f;

    private Rigidbody2D rb;
    [HideInInspector]
    public bool notifyCutsceneFinished;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = patrolPoints[currentPatrolIndex];
    }

   
    void Update()
    {
        if (isPaused || isLocked)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector3)target - transform.position).normalized;
        rb.velocity = direction * speed;

        if (Vector2.Distance(transform.position, target) < .1f)
        {
            StartCoroutine(SetPatrolPoint());
        }
    }

    IEnumerator SetPatrolPoint()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex];
        isPaused = false;
    }
    
    public void MoveToPoint(Vector2 newTarget)
    {
        StopAllCoroutines();
        isPaused = false;
        target = newTarget;
    }

    public void MoveAndStop(Vector2 stopPoint)
    {
        StopAllCoroutines();
        isPaused = false;
        isLocked = false;
        target = stopPoint;
        StartCoroutine(LockWhenArrived());
    }

    IEnumerator LockWhenArrived()
    {
        while (Vector2.Distance(transform.position, target) > .1f)
            yield return null;

        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX
                   | RigidbodyConstraints2D.FreezePositionY
                   | RigidbodyConstraints2D.FreezeRotation;
        isLocked = true;
        if (notifyCutsceneFinished)
        {
            normalCutscene.OnCutsceneFinished();
        }
    }
}