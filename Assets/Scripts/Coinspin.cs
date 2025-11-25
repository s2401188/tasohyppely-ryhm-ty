using UnityEngine;

public class CoinSpinShine : MonoBehaviour
{
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;
    
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

    }
}
