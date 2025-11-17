using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1.0f;
    Rigidbody2D rb;
    bool triggered = false;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (triggered) return;
        triggered = true;
        Invoke("Drop", fallDelay);
    }

    void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
