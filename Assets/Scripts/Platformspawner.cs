using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public GameObject[] enemyPrefabs;

    public float enemySpawnChance = 0.15f;
    public float enemyYOffset = 0.6f;

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

    float highestY;
    Camera cam;
    List<GameObject> platforms = new List<GameObject>();

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
        if (platformPrefabs.Length == 0) return;

        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        float offsetY = Random.Range(minY, maxY);
        if (offsetY < 0.1f) offsetY = 0.1f;

        float y = highestY + offsetY;
        float x = Random.Range(minX, maxX);

        Vector3 pos = new Vector3(x, y, 0);
        GameObject p = Instantiate(prefab, pos, Quaternion.identity);
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
            DespawnBelowCamera d = p.AddComponent<DespawnBelowCamera>();
            d.offsetMultiplier = 1.5f;
        }

        platforms.Add(p);
        highestY = y;

        TrySpawnCollectibleAir(pos);
        TrySpawnEnemy(pos, p);
    }

    void TrySpawnEnemy(Vector3 pos, GameObject platform)
    {
        if (enemyPrefabs.Length == 0) return;
        if (Random.value > enemySpawnChance) return;

        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
            pos + Vector3.up * enemyYOffset,
            Quaternion.identity);

        enemy.transform.SetParent(platform.transform);
    }

    void TrySpawnCollectibleAir(Vector3 platformPos)
    {
        if (collectibleSpawner == null) return;

        if (Random.value < 0.7f)
            collectibleSpawner.SpawnSpecificCollectible(collectibleSpawner.coinPrefab, GetRandomCollectiblePosition(platformPos));

        if (Random.value < 0.3f)
            collectibleSpawner.SpawnSpecificCollectible(collectibleSpawner.heartPrefab, GetRandomCollectiblePosition(platformPos));

        if (Random.value < 0.1f)
            collectibleSpawner.SpawnSpecificCollectible(collectibleSpawner.chestPrefab, GetRandomCollectiblePosition(platformPos));
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
