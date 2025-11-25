using UnityEngine;

public class HeartBobShine : MonoBehaviour
{
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;
    public float shineSpeed = 2f;
    SpriteRenderer sr;
    Vector3 startPos;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.localPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition = startPos + new Vector3(0, yOffset, 0);
        float a = (Mathf.Sin(Time.time * shineSpeed) + 1f) * 0.4f + 0.2f;
        sr.color = new Color(1f, 1f, 1f, a);
    }
}
