using UnityEngine;
using System.Collections;
public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveDefinition[] waves;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private WaveUIHandler uiHandler;
    [SerializeField] private LevelProgression levelProgression;
    private WaveDefinition waveDefinition;

    private int currentWaveIndex = 0;

    private void Start() => StartCoroutine(StartWave());

    private IEnumerator StartWave()
    {
        if (currentWaveIndex >= waves.Length)
            yield break;

        WaveDefinition currentWave = waves[currentWaveIndex];

        uiHandler.PrepareWave(currentWave);

        yield return waveSpawner.SpawnWave(currentWave);

        yield return new WaitUntil(() => waveSpawner.ActiveEnemies == 0);

        levelProgression.OnWaveCompleted(currentWaveIndex);

        yield return uiHandler.RunIntermission(waveDefinition.intermissionLength);

        currentWaveIndex++;
        StartCoroutine(StartWave());
    }
}

[System.Serializable]
public class WaveDefinition
{
    public int zombieCount;
    public int flyingCount;
    public int strongCount;
    public int intermissionLength;
    public GameObject[] objectsToDisable;
    public GameObject[] spawnPointsForThisWave;
}
