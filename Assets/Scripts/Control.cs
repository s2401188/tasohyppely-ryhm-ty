
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public float moveSpeed = 6f;   // Left/right movement speed
    public float jumpForce = 12f;  // How high the player jumps automatically

    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    private Rigidbody2D rb;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();


    }


    void Update()
    {

        // Get input (A/D or Left/Right arrows)
        float inputX = Input.GetAxis("Horizontal");

        // Apply left/right movement
        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);


    }
    private void FixedUpdate()
    {
        if (timeRunning == true)
        {
            if (timePassed < TargetTime)
            {
                timePassed += Time.deltaTime;
            }
            if (timePassed >= TargetTime)
            {
                timeRunning = false;
                timePassed = 0.0f;
            }
        }


    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        // Auto jump only if the player is falling or standing (not going upward)
        if (rb.linearVelocity.y <= 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //if (collision.gameObject.CompareTag("Ground"))

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TP"))
        {
            SceneManager.LoadScene(1);
        }

        
    }
}
