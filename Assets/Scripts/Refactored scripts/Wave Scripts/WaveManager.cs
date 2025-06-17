using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEditor.Overlays;
public class WaveManager : MonoBehaviour
{
    [Header("Barrier Settings")]
    [SerializeField] private float barrierAnimationDuration = 1f;
    [SerializeField] private float barrierAnimationOffset = 10f;

    [SerializeField] private WaveDefinition[] waves;
    public WaveDefinition[] Waves => waves;

    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private WaveUIHandler uiHandler;
    private WaveDefinition waveDefinition;

    public Action<WaveDefinition,int> OnWaveStarted;
    public Action<WaveDefinition,int> OnWaveCompleted;

    private int currentWaveIndex = 0;
    public int CurrentWaveIndex => currentWaveIndex;


    public void SetWaveIndex(int index)
    {
        currentWaveIndex = index;
    }


    public void StartNextWave()
    {
        StartCoroutine(StartWave());
    }

    public IEnumerator StartWave()
    {
        if (currentWaveIndex >= waves.Length)
            yield break;

        Debug.Log($"(Wave Manager) Starting wave {currentWaveIndex} of {waves.Length}");

        WaveDefinition currentWave = waves[currentWaveIndex];

        OnWaveStarted?.Invoke(currentWave, currentWaveIndex);

        yield return waveSpawner.SpawnWave(currentWave);

        yield return new WaitUntil(() => waveSpawner.ActiveEnemies == 0);

        currentWaveIndex++; // Increment after the wave is done

        OnWaveCompleted?.Invoke(currentWave, currentWaveIndex);
        RemoveWaveBarriers(currentWave.objectsToDisable);

    }


    private void RemoveWaveBarriers(GameObject[] objs)
    {
        foreach (var obj in objs)
        {
            obj.transform
                .DOLocalMoveY(obj.transform.localPosition.y + barrierAnimationOffset, barrierAnimationDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() => obj?.SetActive(false));
        }
    }
}


[System.Serializable]
public class WaveDefinition
{
    public int zombieCount;
    public int flyingCount;
    public int strongCount;
    public int intermissionDuration;
    public GameObject[] objectsToDisable;
    public GameObject[] spawnPointsForThisWave;

}
