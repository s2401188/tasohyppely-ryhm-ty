using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public int initialPlatforms = 8;
    public float minX = -2f;
    public float maxX = 2f;
    public float minY = 1.8f;
    public float maxY = 2.6f;

    private float highestY;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        highestY = -3f;
        for (int i = 0; i < initialPlatforms; i++)
            SpawnPlatform();
    }

    void Update()
    {
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        while (highestY < cameraTop + 8f)
            SpawnPlatform();

        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.y < cameraBottom - 10f)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        float x = Random.Range(minX, maxX);
        float y = highestY + Random.Range(minY, maxY);
        Vector3 pos = new Vector3(x, y, 0f);

        int prefabIndex = Random.Range(0, platformPrefabs.Length);
        GameObject p = Instantiate(platformPrefabs[prefabIndex], pos, Quaternion.identity);

        p.tag = "Ground";

        PlatformType typeScript = p.GetComponent<PlatformType>();
        if (typeScript != null)
        {
            if (typeScript.behavior == PlatformBehavior.Normal)
                p.AddComponent<NormalPlatform>();
            else if (typeScript.behavior == PlatformBehavior.Falling)
                p.AddComponent<FallingPlatform>();
            else if (typeScript.behavior == PlatformBehavior.Moving)
                p.AddComponent<MovingPlatform>();
        }

        platforms.Add(p);
        highestY = y;
    }
}
