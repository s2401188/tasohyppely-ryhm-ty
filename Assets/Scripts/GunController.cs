using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public float fireRate = 0.2f;
    float fireCooldown = 0f;
    private AudioSource audioSource;
    public AudioClip GunSound;
    public GameObject GunFire;
    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RotateGun();

        fireCooldown -= Time.deltaTime;
        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }
    private void FixedUpdate()
    {
        if (timeRunning == true)
        {
            GunFire.SetActive(true);

            if (timePassed < TargetTime)
                timePassed += Time.deltaTime;

            if (timePassed >= TargetTime)
            {

                timeRunning = false;
                timePassed = 0.0f;
                GunFire.SetActive (false);
            }
        }
    }
    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletSpeed;
        audioSource.clip = GunSound;
        audioSource.Play();
        timeRunning = true;
    }
}
