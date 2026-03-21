using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;      // assign your coin prefab
    public int numberOfCoins = 10;     // how many coins to spawn
    public Vector2 spawnAreaMin = new Vector2(-100f, -50f);       // bottom-left corner of spawn area
    public Vector2 spawnAreaMax = new Vector2(100f, 50f);       // top-right corner

    void Start()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("CoinSpawner: No prefab assigned!");
            return;
        }

        GenerateCoins();
    }

    void GenerateCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            // randomly picks a spot between max and min

            GameObject coin = Instantiate(
                coinPrefab,
                new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                -0.5f
                ),
                Quaternion.identity);

            coin.transform.localScale = new Vector3(0.2f, 0.2f, 1f);

            SpriteRenderer sr = coin.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 100;
                sr.color = Color.white; // makes sure the layer isn't invisble
            }
        }
    }
}