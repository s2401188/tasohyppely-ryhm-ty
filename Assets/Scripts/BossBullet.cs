using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 8f;

    Vector2 direction;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        rb.linearVelocity = direction * speed;
    }
}
