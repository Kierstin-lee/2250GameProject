using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;      // assign your coin prefab
    public int numberOfCoins = 10;     // how many coins to spawn
    public Vector2 spawnAreaMin;       // bottom-left corner of spawn area
    public Vector2 spawnAreaMax;       // top-right corner
    public float coinScale = 0.5f;     // scale of the coins

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
            Vector3 pos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                -0.5f
            );

            GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity);

            coin.transform.localScale = new Vector3(coinScale, coinScale, 1f);

            SpriteRenderer sr = coin.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 100;
                sr.color = Color.white; // makes sure the layer isn't invisble
            }

            Debug.Log("Generated coin " + i + " at " + pos);
        }
    }
}