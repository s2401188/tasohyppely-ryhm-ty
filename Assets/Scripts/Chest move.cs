using UnityEngine;

public class ChestInfinityMotion : MonoBehaviour
{
    public float speed = 1f;
    public float width = 1f;
    public float height = 0.5f;

    private Vector3 startPos;
    private float t;

    void Start()
    {
        startPos = transform.localPosition;
        t = 0f;
    }

    void Update()
    {
        t += Time.deltaTime * speed;

        float x = Mathf.Sin(t) * width;
        float y = Mathf.Sin(2f * t) * height;

        transform.localPosition = startPos + new Vector3(x, y, 0);
    }
}
