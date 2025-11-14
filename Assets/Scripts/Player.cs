using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using UnityEditor.Experimental.GraphView;

public class PlayerAutoJump : MonoBehaviour
{
    public float moveSpeed = 6f;   // Left/right movement speed
    public float jumpForce = 12f;  // How high the player jumps automatically
    private int CurrentHealth = 1;
    public GameObject PlayerIdle;
    public GameObject PlayerJump;
    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    public int MaxHealth = 2;



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
            PlayerIdle.SetActive(false);
            PlayerJump.SetActive(true);
            if (timePassed < TargetTime)
            {
                timePassed += Time.deltaTime;
            }
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
        // Auto jump only if the player is falling or standing (not going upward)
        if (rb.linearVelocity.y <= 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //if (collision.gameObject.CompareTag("Ground"))
            timeRunning = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HEAL"))
        {
            if (CurrentHealth < MaxHealth)
            {
                CurrentHealth++;
                Debug.Log(CurrentHealth);
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth--;
                Debug.Log(CurrentHealth);
            }
        }

    }
}