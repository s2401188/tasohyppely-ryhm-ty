using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rigidbody;
    private Vector2 movementInput;

    public int CurrentHealth = 1;
    public int MaxHealth = 2;
    private int CollectableItems = 0;
    public int ItemsNeededToWin = 1;
    public int StopSendingNotes = 0;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (CurrentHealth == 0)
        {

            SceneManager.LoadScene(0);
        }

        if (ItemsNeededToWin == 2)
        {
            if (StopSendingNotes == 0)
            {

                SceneManager.LoadScene(1);
                StopSendingNotes = 1;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = movementInput * speed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth--;
                Debug.Log(CurrentHealth);
            }
        }
        if (other.gameObject.CompareTag("GOAL"))
        {
            ItemsNeededToWin++;
        }
    }

}
}
