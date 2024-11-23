using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public int initialEnemies = 10;
    public float timeBetweenWaves = 10f;
    public float timeBetweenEnemies = 0.5f;
    public EnemyAI aiScript;

    private int currentWave = 1;

    void Start()
    {
        // Set the initial speed to 3.5 before starting
        aiScript.baseSpeed = 3.5f;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            int enemiesInWave = initialEnemies * currentWave;
            Debug.Log($"Spawning wave {currentWave} with {enemiesInWave} enemies.");

            for (int i = 0; i < enemiesInWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;

            // Increase the speed from the 2nd wave onwards
            if (currentWave > 1)
            {
                aiScript.baseSpeed++; // You can adjust this increment to your liking
            }
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
