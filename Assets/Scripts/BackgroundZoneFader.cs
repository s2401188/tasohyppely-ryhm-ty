using UnityEngine;

public class BackgroundZoneFader : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer activeRenderer;
    public SpriteRenderer nextRenderer;
    public Sprite[] backgrounds;
    public float[] zoneHeights;
    public float fadeDuration = 2f;

    int currentIndex = 0;
    bool isFading = false;

    void Start()
    {
        if (backgrounds.Length == 0) return;

        activeRenderer.sprite = backgrounds[0];
        SetAlpha(activeRenderer, 1f);
        SetAlpha(nextRenderer, 0f);
    }

    void Update()
    {
        if (isFading || backgrounds.Length == 0) return;

        float y = player.position.y;
        int targetIndex = currentIndex;

        for (int i = 0; i < zoneHeights.Length; i++)
        {
            if (y >= zoneHeights[i])
                targetIndex = i;
        }

        if (targetIndex != currentIndex && targetIndex < backgrounds.Length)
        {
            StartCoroutine(FadeTo(targetIndex));
        }
    }

    System.Collections.IEnumerator FadeTo(int newIndex)
    {
        isFading = true;

        nextRenderer.sprite = backgrounds[newIndex];
        SetAlpha(nextRenderer, 0f);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Clamp01(t / fadeDuration);

            SetAlpha(activeRenderer, 1f - a);
            SetAlpha(nextRenderer, a);

            yield return null;
        }

        activeRenderer.sprite = backgrounds[newIndex];
        SetAlpha(activeRenderer, 1f);
        SetAlpha(nextRenderer, 0f);

        currentIndex = newIndex;
        isFading = false;
    }

    void SetAlpha(SpriteRenderer sr, float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }
}
