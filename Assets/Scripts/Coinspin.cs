using UnityEngine;

public class CoinSpinShine : MonoBehaviour
{
    public float spinSpeed = 180f;
    public float shineSpeed = 2f;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        float a = (Mathf.Sin(Time.time * shineSpeed) + 1f) * 0.4f + 0.2f;
        sr.color = new Color(1f, 1f, 1f, a);
    }
}
