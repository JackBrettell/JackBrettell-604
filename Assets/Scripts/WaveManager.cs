using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        public int zombieCount;
        public int flyingCount;
        public int strongCount;
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnDelay = 1.5f;

    private EnemyFactory enemyFactory;
    private int currentWaveIndex = 0;
    private int activeEnemies = 0;

    void Start()
    {
        enemyFactory = FindFirstObjectByType<EnemyFactory>();
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
            yield break;
        }

        Wave currentWave = waves[currentWaveIndex];
        yield return StartCoroutine(SpawnEnemies(currentWave));

        // Wait until all enemies are defeated before starting the next wave
        while (activeEnemies > 0)
        {
            yield return null;
        }

        currentWaveIndex++;
        StartCoroutine(StartWave()); // Start next wave
    }

    private IEnumerator SpawnEnemies(Wave wave)
    {
        SpawnEnemyType(EnemyType.Zombie, wave.zombieCount);
        SpawnEnemyType(EnemyType.Flying, wave.flyingCount);
        SpawnEnemyType(EnemyType.Strong, wave.strongCount);

        yield return null; // Allow other coroutines to run
    }

    private void SpawnEnemyType(EnemyType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemy = enemyFactory.GetEnemy(type, spawnPoints[randomIndex].position, Quaternion.identity);

            if (enemy != null)
            {
                activeEnemies++;
                enemy.GetComponent<Enemy>().OnDeath += () => activeEnemies--; 
            }

            StartCoroutine(WaitBeforeSpawn());
        }
    }

    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
    }
}
