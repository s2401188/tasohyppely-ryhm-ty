using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBossFight : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    private int CurrentHealth = 1;


    public GameObject PlayerIdle;
    public GameObject PlayerJump;
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;

    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    public int MaxHealth = 3;
    private Rigidbody2D rb;
    private bool timeRunning2 = false;
    private float timePassed2 = 0.0f;
    public float TargetTime2 = 5.0f;
    private bool CanPlayerTakeDamage = true;


    void Start()
    {
        Health1.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = 3;

    }
    void Update()
    {


        if (CurrentHealth == 3)
        {
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(true);
        }
        if (CurrentHealth == 2)
        {
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(false);
        }
        if (CurrentHealth == 1)
        {
            Health1.SetActive(true);
            Health2.SetActive(false);
            Health3.SetActive(false);
        }

        float inputX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        if (timeRunning == true)
        {
            PlayerIdle.SetActive(false);
            PlayerJump.SetActive(true);

            if (timePassed < TargetTime)
                timePassed += Time.deltaTime;

            if (timePassed >= TargetTime)
            {
                PlayerIdle.SetActive(true);
                PlayerJump.SetActive(false);
                timeRunning = false;
                timePassed = 0.0f;
            }
        }
        if (timeRunning2 == true)
        {
            CanPlayerTakeDamage = false;


            if (timePassed2 < TargetTime2)
                timePassed2 += Time.deltaTime;

            if (timePassed2 >= TargetTime2)
            {
                timeRunning2 = false;
                timePassed2 = 0.0f;
                CanPlayerTakeDamage = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.y <= 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            timeRunning = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("HEAL"))
        {
            if (CurrentHealth < MaxHealth) CurrentHealth++;
            if (CurrentHealth >= 2) Health2.SetActive(true);
            if (CurrentHealth == MaxHealth) Health3.SetActive(true);
        }
        if (other.gameObject.CompareTag("FinalBoss"))
        {
            timeRunning2 = true;

            if (CanPlayerTakeDamage == true)
            {

                if (CurrentHealth > 0) CurrentHealth--;
            }
            if (CurrentHealth == 0)
            {
                SceneManager.LoadScene(4);
            }
        }


    }
}
