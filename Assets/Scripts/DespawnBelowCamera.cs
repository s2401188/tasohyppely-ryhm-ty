using UnityEngine;

public class DespawnBelowCamera : MonoBehaviour
{
    public float offsetMultiplier = 1.5f;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float threshold = cam.transform.position.y - cam.orthographicSize * offsetMultiplier;
        if (transform.position.y < threshold) Destroy(gameObject);
    }
}
