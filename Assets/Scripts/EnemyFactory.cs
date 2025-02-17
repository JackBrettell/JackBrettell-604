using UnityEngine;
using System.Collections.Generic;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private int poolSize = 10; // Adjust based on game needs

    private void Start()
    {
        // Pre-instantiate enemies and add to pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject enemy;
        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue();
        }
        else
        {
            enemy = Instantiate(enemyPrefab);
        }

        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.SetActive(true);
        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
