using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float deathUpForce = 6f;
    public float deathFallGravity = 3f;
    bool dead = false;

    Rigidbody2D rb;
    Collider2D col;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        cam = Camera.main;
    }

   
    public void TakeDamage(int amount)
    {
        if (dead) return;

        health -= amount;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        dead = true;

        col.enabled = false; 
        rb.gravityScale = deathFallGravity;

        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, deathUpForce);

        
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);

       
        EnemyWalker walker = GetComponent<EnemyWalker>();
        if (walker != null) walker.enabled = false;
    }

    void Update()
    {
        
        if (dead && transform.position.y < cam.transform.position.y - cam.orthographicSize - 2f)
            Destroy(gameObject);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dead && collision.gameObject.CompareTag("Ground"))
            transform.SetParent(collision.transform);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            transform.SetParent(null);
    }
}
