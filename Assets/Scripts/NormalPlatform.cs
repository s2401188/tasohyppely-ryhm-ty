using UnityEngine;

public class NormalPlatform : MonoBehaviour
{
    public int hits = 0;
    public int maxHits = 5;
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (!col.collider.CompareTag("Player")) return;

        hits++;

        if (hits >= maxHits)
            rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
