using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private PlayerState playerState;

    private Rigidbody2D rb;
    public PlayerCombat playerCombat;
    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerCombat.Attack();
        }
    }

    private void FixedUpdate()
    {
        if (playerState.currentState != PlayerState.State.Normal)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float horizontalMove = 0;
        float verticalMove = 0;

        if (Input.GetKey(KeyCode.A)) horizontalMove = -1;
        if (Input.GetKey(KeyCode.D)) horizontalMove = 1;
        if (Input.GetKey(KeyCode.S)) verticalMove = -1;
        if (Input.GetKey(KeyCode.W)) verticalMove = 1;
        
        Vector2 move = new Vector2(horizontalMove, verticalMove);
        if (move.magnitude > 1) move.Normalize();

        if (move.x != 0)
        {
            float sign = Mathf.Sign(move.x);
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * sign,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        rb.velocity = move * moveSpeed;
    }
}