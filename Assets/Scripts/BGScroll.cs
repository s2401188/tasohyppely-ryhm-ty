using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
