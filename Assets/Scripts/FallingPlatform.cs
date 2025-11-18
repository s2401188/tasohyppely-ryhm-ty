using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 3.0f;
    Rigidbody2D rb;
    Collider2D col;
    bool triggered = false;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (triggered) return;
        triggered = true;
        Invoke("Drop", fallDelay);
    }

    void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        col.isTrigger = true;
    }
}