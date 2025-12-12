using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float chargeTime = 0.7f;
    public float fireTime = 1.2f;
    public Sprite chargeSprite;
    public Sprite fireSprite;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FireRoutine());
    }

    System.Collections.IEnumerator FireRoutine()
    {
        sr.sprite = chargeSprite;
        yield return new WaitForSeconds(chargeTime);
        sr.sprite = fireSprite;
        yield return new WaitForSeconds(fireTime);
        Destroy(gameObject);
    }
}
