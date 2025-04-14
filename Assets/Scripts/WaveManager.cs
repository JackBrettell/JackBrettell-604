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
        public int intermissionLength;
        public GameObject[] objectsToDisable;
        public GameObject[] spawnPointsForThisWave;
    }

    [SerializeField] private IntermissionUI intermissionArrow;
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minSpawnDelay = 0.5f;
    [SerializeField] private float maxSpawnDelay = 2f;
    [SerializeField] private GameObject player;
    [SerializeField] private LevelProgression levelProgression;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private HUD hud;

    public int currentWaveIndex = 0;
    private int activeEnemies = 0;

    public delegate void RoundOverEvent();
    public event RoundOverEvent OnRoundFinished;

    [Header("Unlock animation")]
    [SerializeField] private float unlockAnimationDuration = 1f;
    [SerializeField] private float unlockAnimationOffset = 10f;

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

        // Disable specified objects
        foreach (GameObject obj in currentWave.objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        yield return StartCoroutine(SpawnEnemies(currentWave));

        while (activeEnemies > 0)
            yield return null;

        OnRoundFinished?.Invoke();
        StartCoroutine(Intermission(currentWave));
    }


    private IEnumerator SpawnEnemies(Wave wave)
    {
        yield return StartCoroutine(SpawnEnemyType(EnemyType.Zombie, wave.zombieCount, wave.spawnPointsForThisWave));
        yield return StartCoroutine(SpawnEnemyType(EnemyType.Flying, wave.flyingCount, wave.spawnPointsForThisWave));
        yield return StartCoroutine(SpawnEnemyType(EnemyType.Strong, wave.strongCount, wave.spawnPointsForThisWave));
    }

    private IEnumerator SpawnEnemyType(EnemyType type, int count, GameObject[] spawnArray)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, spawnArray.Length);
            Transform spawnPoint = spawnArray[randomIndex].transform;

            EnemyBase enemy = enemyFactory.GetEnemy(type, spawnPoint.position, Quaternion.identity);

            if (enemy != null)
            {
                activeEnemies++;
                enemy.OnDeath += () => activeEnemies--;
            }

            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    

    private IEnumerator Intermission(Wave wave)
    {
        intermissionArrow.ToggleArrow();
        hud.showIntermission();

        hud.StartIntermissionTimer(wave.intermissionLength);
        yield return new WaitForSeconds(wave.intermissionLength);

        intermissionArrow.ToggleArrow();
        hud.hideIntermission();

        levelProgression.OnWaveCompleted(currentWaveIndex);
        currentWaveIndex++;
        StartCoroutine(StartWave());
    }
}
