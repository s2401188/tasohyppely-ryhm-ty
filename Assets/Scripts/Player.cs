using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using UnityEditor.Experimental.GraphView;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UIElements;

public class PlayerAutoJump : MonoBehaviour
{
    public float moveSpeed = 6f;   // Left/right movement speed
    public float jumpForce = 12f;  // How high the player jumps automatically
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
        // Update the count text with the current count.
        countText.text = "Score: " + count.ToString();
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
            Health1.SetActive(true ); 
            Health2.SetActive(true );
            Health3.SetActive(false );
        }

        if (CurrentHealth == 1)
        { 
            Health1.SetActive(true);
            Health2.SetActive(false);
            Health3.SetActive(false);
        }
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
        if (other.gameObject.CompareTag("TP"))
        {
            SceneManager.LoadScene(1);
        }
        if (other.gameObject.CompareTag("Cheater"))
        {
            SceneManager.LoadScene(6);
        }


        if (other.gameObject.CompareTag("HEAL"))
        {
            if (CurrentHealth < MaxHealth)
            {
                CurrentHealth++;
                Health2.SetActive(true);
                Debug.Log(CurrentHealth);

            }
            if (CurrentHealth == MaxHealth)
            {
                Health3.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("Score"))
        {
            count = count + 50;
            SetCountText();
        }
        if(other.gameObject.CompareTag("Chest"))
        {

            count = count + 100;
            SetCountText();
            if (CurrentHealth < MaxHealth)
            {
                CurrentHealth++;
            }
        }

        if (other.gameObject.CompareTag("Spikes"))
        {
            if (CurrentHealth <= 3)
            {
                CurrentHealth--;
            }
            if (CurrentHealth == 0)
            {
                SceneManager.LoadScene(3);
            }

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (CurrentHealth <= 3)
            {
                CurrentHealth--;
            }
            if (CurrentHealth == 0)
            {
                Debug.Log("Bot killed you");

            }

        }



        if (other.gameObject.CompareTag("FinalBoss"))
            {
                SceneManager.LoadScene(4);
            }
            if (other.gameObject.CompareTag("Bottom"))
            {
                SceneManager.LoadScene(2);

            }
                

        }
    }
