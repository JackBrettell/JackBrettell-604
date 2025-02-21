using UnityEngine;
using System.Collections.Generic;

public enum EnemyType
{
    Zombie,
    Flying,
    Strong
}

public class EnemyFactory : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyPrefab
    {
        public EnemyType type;
        public GameObject prefab;
    }

    [SerializeField] private EnemyPrefab[] enemyPrefabs;
    private Dictionary<EnemyType, Queue<GameObject>> enemyPools = new Dictionary<EnemyType, Queue<GameObject>>();
    private Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>();

    private int poolSize = 20;

    private void Awake()
    {
        // Initialize dictionaries
        foreach (var enemy in enemyPrefabs)
        {
            enemyPrefabDict[enemy.type] = enemy.prefab;
            enemyPools[enemy.type] = new Queue<GameObject>();

            // Preload enemies into pool
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(enemy.prefab);
                obj.SetActive(false);
                enemyPools[enemy.type].Enqueue(obj);
            }
        }
    }

    public GameObject GetEnemy(EnemyType type, Vector3 position, Quaternion rotation)
    {
 
        GameObject enemy;

        if (enemyPools[type].Count > 0)
        {
            enemy = enemyPools[type].Dequeue();
        }
        else
        {
            enemy = Instantiate(enemyPrefabDict[type]);
        }

        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.SetActive(true);

        // Assign enemy type before returning
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.enemyType = type; 
        }

        return enemy;
    }


    public void ReturnEnemy(EnemyType type, GameObject enemy)
    {
        Debug.Log("Returning enemy to pool");
        enemy.SetActive(false);
        enemyPools[type].Enqueue(enemy);
    }
}
