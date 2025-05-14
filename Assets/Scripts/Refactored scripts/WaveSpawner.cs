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
        DisableObjectsWithAnimation(wave.objectsToDisable);

        yield return SpawnEnemies(EnemyType.Zombie, wave.zombieCount, wave.spawnPointsForThisWave);
        yield return SpawnEnemies(EnemyType.Flying, wave.flyingCount, wave.spawnPointsForThisWave);
        yield return SpawnEnemies(EnemyType.Strong, wave.strongCount, wave.spawnPointsForThisWave);
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

    private void DisableObjectsWithAnimation(GameObject[] objs)
    {
        foreach (var obj in objs)
        {
            obj.transform
                .DOLocalMoveY(obj.transform.localPosition.y + 10f, 1f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => obj?.SetActive(false));
        }
    }
}
