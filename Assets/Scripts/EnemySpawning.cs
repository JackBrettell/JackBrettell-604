using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private int maxEnemies = 5;




    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                int randomIndex = Random.Range(0, spawnPoint.Length);
                Instantiate(enemyPrefab, spawnPoint[randomIndex].transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
