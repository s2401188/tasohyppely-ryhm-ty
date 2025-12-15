using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float minX = -6f;
    public float maxX = 6f;
    public float minY = -4f;
    public float maxY = 4f;
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;
    private int CurrentHealth = 1;
    private bool timeRunning2 = false;
    private float timePassed2 = 0.0f;
    public float TargetTime2 = 5.0f;
    private bool CanPlayerTakeDamage = true;

    Rigidbody2D rb;
    Vector2 input;

    void Start()
    {
        Health1.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = 3;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
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

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;
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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
        Vector3 pos = transform.position;
        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
       // pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
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
}
