using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float range = 2f;
    float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        float x = startX + Mathf.Sin(Time.time * speed) * range;
        transform.position = new Vector3(x, transform.position.y, 0f);
    }
}
