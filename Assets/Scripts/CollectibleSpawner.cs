using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject heartPrefab;
    public GameObject chestPrefab;

    public float chestChance = 0.1f;
    public float heartChance = 0.2f;

    public void SpawnCollectible(Vector3 position)
    {
        float r = Random.value;
        if (r < chestChance)
            Instantiate(chestPrefab, position, Quaternion.identity);
        else if (r < chestChance + heartChance)
            Instantiate(heartPrefab, position, Quaternion.identity);
        else
            Instantiate(coinPrefab, position, Quaternion.identity);
    }
}

