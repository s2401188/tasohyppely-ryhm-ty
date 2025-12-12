using UnityEngine;

public class BossMovementController : MonoBehaviour
{
    public Vector2 floatAreaMin = new Vector2(-6f, 0f);
    public Vector2 floatAreaMax = new Vector2(6f, 3f);
    public float horizontalSpeed = 1.5f;
    public float verticalAmplitude = 0.8f;
    public float verticalFrequency = 1.2f;
    public float moveToTargetSpeed = 5f;
    public Vector2 centerPosition = new Vector2(0f, 2f);
    public Vector2 centerTopPosition = new Vector2(0f, 4f);

    bool lockToTarget;
    Vector2 targetPosition;
    float baseY;
    float timeOffset;

    void Start()
    {
        baseY = Mathf.Clamp(transform.position.y, floatAreaMin.y, floatAreaMax.y);
        timeOffset = Random.value * 10f;
    }

    void Update()
    {
        if (lockToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveToTargetSpeed * Time.deltaTime);
            return;
        }

        float width = floatAreaMax.x - floatAreaMin.x;
        float px = Mathf.PingPong(Time.time * horizontalSpeed, width) + floatAreaMin.x;
        float py = baseY + Mathf.Sin(Time.time * verticalFrequency + timeOffset) * verticalAmplitude;
        transform.position = new Vector3(px, py, transform.position.z);
    }

    public void GoToCenter()
    {
        lockToTarget = true;
        targetPosition = centerPosition;
    }

    public void GoToCenterTop()
    {
        lockToTarget = true;
        targetPosition = centerTopPosition;
    }

    public void Release()
    {
        lockToTarget = false;
        baseY = transform.position.y;
    }
}
