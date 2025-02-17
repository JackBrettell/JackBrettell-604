using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private int maxEnemies = 5;

    private EnemyFactory enemyFactory;

    void Start()
    {
        enemyFactory = FindFirstObjectByType<EnemyFactory>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                EnemyType randomType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);

                enemyFactory.GetEnemy(randomType, spawnPoints[randomIndex].position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
