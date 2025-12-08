using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerAutoJump : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    private int CurrentHealth = 1;

    public GameObject PlayerIdle;
    public GameObject PlayerJump;
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;
    public GameObject ScoreCrown;

    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    public int MaxHealth = 3;

    int count = 0;
    public TextMeshProUGUI countText;

    private Rigidbody2D rb;

    void Start()
    {
        Health1.SetActive(true);
        SetCountText();
        rb = GetComponent<Rigidbody2D>();
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }

    void Update()
    {
        if (count >= 1000) ScoreCrown.SetActive(true);

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

        // --------------------------------------------------------
        // SCREEN WRAP (Doodle Jump style left/right teleport)
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        Vector3 pos = transform.position;

        if (pos.x > halfWidth) pos.x = -halfWidth;
        else if (pos.x < -halfWidth) pos.x = halfWidth;

        transform.position = pos;
        // --------------------------------------------------------
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
        if (other.gameObject.CompareTag("TP")) SceneManager.LoadScene(1);
        if (other.gameObject.CompareTag("Cheater")) SceneManager.LoadScene(6);

        if (other.gameObject.CompareTag("HEAL"))
        {
            if (CurrentHealth < MaxHealth) CurrentHealth++;
            if (CurrentHealth >= 2) Health2.SetActive(true);
            if (CurrentHealth == MaxHealth) Health3.SetActive(true);
        }

        if (other.gameObject.CompareTag("Score"))
        {
            count += 50;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Chest"))
        {
            count += 100;
            SetCountText();
            if (CurrentHealth < MaxHealth) CurrentHealth++;
        }

        if (other.gameObject.CompareTag("Spikes") || other.gameObject.CompareTag("Enemy"))
        {
            if (CurrentHealth > 0) CurrentHealth--;
            if (CurrentHealth == 0) SceneManager.LoadScene(other.CompareTag("Spikes") ? 3 : 7);
        }

        if (other.gameObject.CompareTag("FinalBoss")) SceneManager.LoadScene(4);
        if (other.gameObject.CompareTag("Bottom")) SceneManager.LoadScene(2);
    }
}