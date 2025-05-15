using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using Unity.VisualScripting;
public class WaveManager : MonoBehaviour
{
    [Header("Barrier Settings")]
    [SerializeField] private float barrierAnimationDuration = 1f;
    [SerializeField] private float barrierAnimationOffset = 10f;

    [SerializeField] private WaveDefinition[] waves;
    public WaveDefinition[] Waves => waves;

    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private WaveUIHandler uiHandler;
    [SerializeField] private LevelProgression levelProgression;
    private WaveDefinition waveDefinition;

    public Action<WaveDefinition> OnWaveCompleted;

    private int currentWaveIndex = 0;

    private void Start() => StartCoroutine(StartWave());

    public IEnumerator StartWave()
    {
        if (currentWaveIndex >= waves.Length)
            yield break;

        WaveDefinition currentWave = waves[currentWaveIndex];


        yield return waveSpawner.SpawnWave(currentWave);

        yield return new WaitUntil(() => waveSpawner.ActiveEnemies == 0);

        OnWaveCompleted?.Invoke(currentWave);

        RemoveWaveBarriers(currentWave.objectsToDisable);


        currentWaveIndex++;
        StartCoroutine(StartWave());
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
