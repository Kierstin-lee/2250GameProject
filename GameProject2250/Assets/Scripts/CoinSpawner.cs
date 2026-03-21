using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;      // assign your coin prefab
    public int numberOfCoins = 10;     // how many coins to spawn
    public Vector2 spawnAreaMin;       // bottom-left corner of spawn area
    public Vector2 spawnAreaMax;       // top-right corner

    void Start()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f // make sure Z is 0 for 2D
            );
            GameObject newCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

            newCoin.transform.localScale = new Vector3(5, 5, 5);
            SpriteRenderer sr = newCoin.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = Color.red;

            Debug.Log("Coin spawned at: " + spawnPos);
        }
    }
}