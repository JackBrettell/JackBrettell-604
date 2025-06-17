using DG.Tweening;
using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory factory;
    [SerializeField] private float minSpawnDelay, maxSpawnDelay;

    public int ActiveEnemies { get; private set; }

    public IEnumerator SpawnWave(WaveDefinition wave)
    {
        Debug.LogWarning($"Spawning {wave.zombieCount} zombies, " +
                  $"{wave.flyingCount} flying, and {wave.strongCount} strong enemies.");
        yield return SpawnEnemies(EnemyType.Zombie, wave.zombieCount, wave.spawnPointsForThisWave);
        Debug.Log($"Spawning zombies at {wave.spawnPointsForThisWave.Length} spawn points." + wave.spawnPointsForThisWave);
        //  yield return SpawnEnemies(EnemyType.Flying, wave.flyingCount, wave.spawnPointsForThisWave);
        //  yield return SpawnEnemies(EnemyType.Strong, wave.strongCount, wave.spawnPointsForThisWave);
    }

    private IEnumerator SpawnEnemies(EnemyType type, int count, GameObject[] spawnPoints)
    {
        for (int i = 0; i < count; i++)
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
            var enemy = factory.GetEnemy(type, point.position, Quaternion.identity);

            if (enemy != null)
            {
                ActiveEnemies++;
                enemy.OnDeath += () => ActiveEnemies--;
            }

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }


}
