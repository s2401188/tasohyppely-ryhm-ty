using UnityEngine;

public class ChestInfinityMotion : MonoBehaviour
{
    public float speed = 1f;
    public float size = 1f;
    public float shineSpeed = 2f;
    SpriteRenderer sr;
    float t;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        t += Time.deltaTime * speed;
        float x = Mathf.Sin(t) * size;
        float y = Mathf.Sin(t * 2f) * size * 0.5f;
        transform.localPosition += new Vector3(x, y, 0) * Time.deltaTime;
        float a = (Mathf.Sin(Time.time * shineSpeed) + 1f) * 0.4f + 0.2f;
        sr.color = new Color(1f, 1f, 1f, a);
    }
}
