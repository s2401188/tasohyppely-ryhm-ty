using UnityEngine;

public class BossAttackDirector : MonoBehaviour
{
    public BossHealth health;

    public Transform fireCenter;
    public Transform fireLeft;
    public Transform fireRight;
    public Transform fireTop;

    public GameObject bulletPrefab;
    public GameObject laserPrefab;
    public GameObject lightningPrefab;

    public float fireRate = 6f;
    float t;

    public bool attacksEnabled;

    void Awake()
    {
        if (health == null) health = GetComponent<BossHealth>();
    }

    void Update()
    {
        if (!attacksEnabled) return;

        t += Time.deltaTime;
        if (t >= 1f / Mathf.Max(0.01f, fireRate))
        {
            t = 0f;
            FireSimpleTest();
        }
    }

    void FireSimpleTest()
    {
        if (bulletPrefab == null || fireCenter == null) return;
        Instantiate(bulletPrefab, fireCenter.position, fireCenter.rotation);
    }
}
