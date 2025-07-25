using UnityEngine;
using System.Collections.Generic;

public enum EnemyType
{
    Zombie,
    // Flying
    //Strong
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
    [SerializeField] private GameObject player;

    private Dictionary<EnemyType, Queue<EnemyBase>> enemyPools = new();
    private Dictionary<EnemyType, EnemyBase> enemyPrefabDict = new();

    private int poolSize = 200;

    private void Awake()
    {
        foreach (var enemy in enemyPrefabs)
        {
            enemyPrefabDict[enemy.type] = enemy.prefab;
            enemyPools[enemy.type] = new Queue<EnemyBase>();

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

        enemy.Initialize(this, player);
        enemy.GetComponent<EnemyBase>().enabled = true;

        enemy.gameObject.SetActive(true);

        enemy.enemyType = type;

        return enemy;
    }

    public void ReturnEnemy(EnemyType type, EnemyBase enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPools[type].Enqueue(enemy);
    }
}
