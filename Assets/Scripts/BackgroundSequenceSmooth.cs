using UnityEngine;

public class BackgroundSequenceSmooth : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer[] backgrounds;
    public float[] zoneHeights;
    public float fadeDuration = 2f;

    int currentIndex = 0;
    float bgHeight;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        bgHeight = backgrounds[0].bounds.size.y;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            Color c = backgrounds[i].color;
            c.a = (i == 0 ? 1f : 0f);
            backgrounds[i].color = c;
        }
    }

    void Update()
    {
        LoopBackground(backgrounds[currentIndex]);
        CheckForTransition();
    }

    void LoopBackground(SpriteRenderer bg)
    {
        float camY = cam.transform.position.y;
        if (bg.transform.position.y + bgHeight < camY)
        {
            bg.transform.position = new Vector3(
                bg.transform.position.x,
                bg.transform.position.y + bgHeight * 2f,
                bg.transform.position.z
            );
        }
    }

    void CheckForTransition()
    {
        float y = player.position.y;

        if (currentIndex < zoneHeights.Length - 1)
        {
            if (y >= zoneHeights[currentIndex + 1])
            {
                StartCoroutine(FadeToNext(currentIndex + 1));
            }
        }
    }

    System.Collections.IEnumerator FadeToNext(int nextIndex)
    {
        SpriteRenderer from = backgrounds[currentIndex];
        SpriteRenderer to = backgrounds[nextIndex];

        float t = 0f;
        while (t < fadeDuration)
        {
            float a = t / fadeDuration;

            Color c1 = from.color;
            Color c2 = to.color;

            c1.a = 1f - a;
            c2.a = a;

            from.color = c1;
            to.color = c2;

            t += Time.deltaTime;
            yield return null;
        }

        Color f = from.color;
        f.a = 0f;
        from.color = f;

        Color n = to.color;
        n.a = 1f;
        to.color = n;

        currentIndex = nextIndex;
    }
}
