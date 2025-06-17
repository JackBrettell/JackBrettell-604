using UnityEngine;
using System.Collections.Generic;

public enum EnemyType
{
    Zombie,
    //Flying,
   // Strong
}

public class EnemyFactory : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyPrefab
    {
        public EnemyType type;
        public EnemyBase prefab;
    }

    [SerializeField] private EnemyPrefab[] enemyPrefabs;
    private Dictionary<EnemyType, Queue<EnemyBase>> enemyPools = new();
    private Dictionary<EnemyType, EnemyBase> enemyPrefabDict = new();

    [SerializeField] private GameObject player;
    private int poolSize = 200;

    private void Awake()
    {
        // Initialize dictionaries
        foreach (var enemy in enemyPrefabs)
        {
            enemyPrefabDict[enemy.type] = enemy.prefab;
            enemyPools[enemy.type] = new Queue<EnemyBase>();

            // Preload enemies into pool
            for (int i = 0; i < poolSize; i++)
            {
                EnemyBase obj = Instantiate(enemy.prefab);
                



                obj.gameObject.SetActive(false);
                enemyPools[enemy.type].Enqueue(obj);
            }
        }
    }

    public EnemyBase GetEnemy(EnemyType type, Vector3 position, Quaternion rotation)
    {

        EnemyBase enemy;

       

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
        EnemyBase enemyScript = enemy.GetComponent<EnemyBase>();
        if (enemyScript != null)
        {
            enemyScript.enemyType = type; 
        }
        enemy.Initialize(this, player);


        return enemy;
    }


    public void ReturnEnemy(EnemyType type, EnemyBase enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPools[type].Enqueue(enemy);
    }
}
