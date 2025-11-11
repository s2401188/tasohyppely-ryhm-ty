using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float minX = -2f;
    public float maxX = 2f;
    public float minY = 1.5f;
    public float maxY = 2.2f;

    private float highestY = 0f;

    void Update()
    {
        if (Camera.main.transform.position.y + 10f > highestY)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        highestY += y;

        Instantiate(platformPrefab, new Vector3(x, highestY, 0), Quaternion.identity);
    }
}

