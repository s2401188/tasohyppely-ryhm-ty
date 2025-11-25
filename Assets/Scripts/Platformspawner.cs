using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public int initialPlatforms = 8;
    public float minX = -3f;
    public float maxX = 3f;
    public float minY = 1.5f;
    public float maxY = 3.5f;
    public float topBuffer = 12f;
    public int spawnSafetyLimitPerFrame = 50;
    public float minCollectibleX = -3f;
    public float maxCollectibleX = 3f;
    public float minCollectibleY = 0f;
    public float maxCollectibleY = 20f;

    public CollectibleSpawner collectibleSpawner;

    private float highestY;
    private Camera cam;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        cam = Camera.main;
        highestY = cam.transform.position.y - cam.orthographicSize - 1f;
        for (int i = 0; i < initialPlatforms; i++)
            SpawnPlatform();
    }

    void Update()
    {
        float cameraTop = cam.transform.position.y + topBuffer;
        int safety = 0;
        while (highestY < cameraTop && safety < spawnSafetyLimitPerFrame)
        {
            SpawnPlatform();
            safety++;
        }
    }

    void SpawnPlatform()
    {
        if (platformPrefabs == null || platformPrefabs.Length == 0) return;
        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        float offsetY = Random.Range(minY, maxY);
        if (offsetY < 0.1f) offsetY = 0.1f;
        float y = highestY + offsetY;
        float x = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(x, y, 0);
        GameObject p = Instantiate(prefab, spawnPos, Quaternion.identity);
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
        else
        {
            int type = Random.Range(0, 3);
            if (type == 0) p.AddComponent<NormalPlatform>();
            else if (type == 1) p.AddComponent<FallingPlatform>();
            else p.AddComponent<MovingPlatform>();
        }
        if (p.GetComponent<DespawnBelowCamera>() == null)
        {
            DespawnBelowCamera db = p.AddComponent<DespawnBelowCamera>();
            db.offsetMultiplier = 1.5f;
        }
        platforms.Add(p);
        highestY = y;

        TrySpawnCollectibleAir(spawnPos);
    }

    void TrySpawnCollectibleAir(Vector3 platformPos)
    {
        if (collectibleSpawner == null) return;

        if (Random.value < 0.7f) // 70% chance for coin
        {
            Vector3 coinPos = GetRandomCollectiblePosition(platformPos);
            collectibleSpawner.SpawnSpecificCollectible(collectibleSpawner.coinPrefab, coinPos);
        }

        if (Random.value < 0.3f) // 30% chance for heart
        {
            Vector3 heartPos = GetRandomCollectiblePosition(platformPos);
            collectibleSpawner.SpawnSpecificCollectible(collectibleSpawner.heartPrefab, heartPos);
        }

        if (Random.value < 0.1f) // 10% chance for chest
        {
            Vector3 chestPos = GetRandomCollectiblePosition(platformPos);
            collectibleSpawner.SpawnSpecificCollectible(collectibleSpawner.chestPrefab, chestPos);
        }
    }

    Vector3 GetRandomCollectiblePosition(Vector3 platformPos)
    {
        float offsetX = Random.Range(-2f, 2f);
        float offsetY = Random.Range(1f, 4f);
        float spawnX = Mathf.Clamp(platformPos.x + offsetX, minCollectibleX, maxCollectibleX);
        float spawnY = Mathf.Clamp(platformPos.y + offsetY, minCollectibleY, maxCollectibleY);
        return new Vector3(spawnX, spawnY, 0);
    }
}
