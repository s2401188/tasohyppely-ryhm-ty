using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    public float speed = 1.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkDistance = 0.2f;

    Rigidbody2D rb;
    bool facingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2((facingRight ? 1 : -1) * speed, rb.linearVelocity.y);

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);
        if (!hit.collider)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
