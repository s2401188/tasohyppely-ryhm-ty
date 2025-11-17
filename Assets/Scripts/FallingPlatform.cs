using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
