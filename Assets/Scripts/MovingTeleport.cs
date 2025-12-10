
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingTeleport : MonoBehaviour
{
    public int Taso = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(Taso);
        }
    }
}
