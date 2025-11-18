using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f; // Vihollisen nopeus
    private Transform playerTransform;

    void Start()
    {
        // Etsit‰‰n pelaaja, jolla on tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("tag error");
        }
    }

    void Update()
    {
        if (GameObject.Find("Trigger").GetComponent<CanFinalBossAttack>().HasEntered)
        {
            if (playerTransform != null)
            {
                // Lasketaan suunta pelaajaan
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                // Liikutaan kohti pelaajaa
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
            }
        }
    }
}
