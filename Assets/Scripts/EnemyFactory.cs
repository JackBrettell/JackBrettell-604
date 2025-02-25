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
        public Enemy prefab;
    }

    [SerializeField] private EnemyPrefab[] enemyPrefabs;
    private Dictionary<EnemyType, Queue<Enemy>> enemyPools = new();
    private Dictionary<EnemyType, Enemy> enemyPrefabDict = new();

    private int poolSize = 20;

    private void Awake()
    {
        // Initialize dictionaries
        foreach (var enemy in enemyPrefabs)
        {
            enemyPrefabDict[enemy.type] = enemy.prefab;
            enemyPools[enemy.type] = new Queue<Enemy>();

            // Preload enemies into pool
            for (int i = 0; i < poolSize; i++)
            {
                Enemy obj = Instantiate(enemy.prefab);
                



                obj.gameObject.SetActive(false);
                enemyPools[enemy.type].Enqueue(obj);
            }
        }
    }

    public Enemy GetEnemy(EnemyType type, Vector3 position, Quaternion rotation)
    {

        Enemy enemy;

       

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
        enemy.gameObject.SetActive(true);

        // Assign enemy type before returning
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.enemyType = type; 
        }

        return enemy;
    }


    public void ReturnEnemy(EnemyType type, Enemy enemy)
    {
        Debug.Log("Returning enemy to pool");
        enemy.gameObject.SetActive(false);
        enemyPools[type].Enqueue(enemy);
    }
}
