using UnityEngine;
using UnityEngine.UI;

public class AltimeterUI : MonoBehaviour
{
    public Transform player;
    public Image fillImage;
    public RectTransform marker;
    public RectTransform icon;
    public float maxHeight = 600f;

    float startY;
    float smoothT;

    void Start()
    {
        startY = player.position.y;
        smoothT = 0f;
    }

    void Update()
    {
        float currentHeight = Mathf.Max(0, player.position.y - startY);
        float t = Mathf.Clamp01(currentHeight / maxHeight);

        smoothT = Mathf.Lerp(smoothT, t, Time.deltaTime * 5f);

        fillImage.fillAmount = smoothT;

        marker.anchorMin = new Vector2(0, smoothT);
        marker.anchorMax = new Vector2(1, smoothT);

        icon.anchorMin = new Vector2(0.5f, smoothT);
        icon.anchorMax = new Vector2(0.5f, smoothT);
    }
}
