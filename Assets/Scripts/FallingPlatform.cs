using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.3f;
    public float respawnDelay = 1f;

    Rigidbody2D rb;
    Collider2D col;
    Vector3 startPos;
    bool triggered = false;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        col = GetComponent<Collider2D>();
        startPos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (triggered) return;

        
        if (!other.collider.CompareTag("Player")) return;

        
        if (other.contacts[0].normal.y <= -0.5f)
        {
            triggered = true;
            Invoke("Drop", fallDelay);
        }
    }

    void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        col.isTrigger = true;
        Invoke("Respawn", respawnDelay);
    }

    void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPos;
        col.isTrigger = false;
        triggered = false;
    }
}
