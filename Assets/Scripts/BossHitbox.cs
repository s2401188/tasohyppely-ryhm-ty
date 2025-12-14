using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public BossHealth boss;

    void Awake()
    {
        if (boss == null) boss = GetComponentInParent<BossHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Bullet b = other.GetComponent<Bullet>();
        if (b == null) return;

        if (boss != null && boss.TryTakeDamage(b.damage))
            Destroy(other.gameObject);
        else
            Destroy(other.gameObject);
    }
}
