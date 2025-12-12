using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 6f;

    Vector2 direction;
    bool hasDirection;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        hasDirection = true;
    }

    void Update()
    {
        if (!hasDirection) return;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
