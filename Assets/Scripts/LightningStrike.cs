using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public float warningTime = 0.4f;
    public float strikeTime = 0.2f;
    public Sprite warningSprite;
    public Sprite strikeSprite;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(StrikeRoutine());
    }

    System.Collections.IEnumerator StrikeRoutine()
    {
        sr.sprite = warningSprite;
        yield return new WaitForSeconds(warningTime);
        sr.sprite = strikeSprite;
        yield return new WaitForSeconds(strikeTime);
        Destroy(gameObject);
    }
}
