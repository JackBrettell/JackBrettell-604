using UnityEngine;
using System.Collections;
using static EnemyBase;
using Unity.VisualScripting;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        public int zombieCount;
        public int flyingCount;
        public int strongCount;
        public int intermissionLength; // INCLUDE INTERMISSION
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private LevelProgression levelProgression;
    [SerializeField] private EnemyFactory enemyFactory;
    public int currentWaveIndex = 0;
    private int activeEnemies = 0;

    public delegate void RoundOverEvent();
    public event RoundOverEvent OnRoundFinished;


    void Start()
    {
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

        OnRoundFinished?.Invoke();
        levelProgression.OnWaveCompleted(currentWaveIndex);
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
            EnemyBase enemy = enemyFactory.GetEnemy(type, spawnPoints[randomIndex].position, Quaternion.identity);



            if (enemy != null)
            {
                activeEnemies++;
                enemy.GetComponent<EnemyBase>().OnDeath += () => activeEnemies--; 
            }

            StartCoroutine(WaitBeforeSpawn());
        }
    }

    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
    }


}
