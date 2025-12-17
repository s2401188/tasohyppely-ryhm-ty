using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyBullets : MonoBehaviour
{
    public bool CanPlayerDestroy = true;
    public bool CanMapBorderDestory = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && CanPlayerDestroy)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("MB") && CanMapBorderDestory)
        {
            Destroy(gameObject);
        }

    }
}
