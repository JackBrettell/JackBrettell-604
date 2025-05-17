using System;
using UnityEngine;

public class WaveMediator : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private HUDMediator hudMediator;

    // ==== Events ====
    // Wave
    public event Action<WaveDefinition> OnWaveStarted;
    public event Action<WaveDefinition> OnWaveEnded;
    public event Action<int> OnUpdateWaveNum;
    public int CurrentWaveNumber = 0;
    // Intermission
    public event Action<WaveDefinition> OnIntermissionStarted;
    public event Action<WaveDefinition> OnIntermissionEnded;


    private void OnEnable()
    {
        waveManager.OnWaveCompleted += HandleWaveCompleted;

    }

    private void OnDisable()
    {
        waveManager.OnWaveCompleted -= HandleWaveCompleted;
    }

    // ===== Wave =====
    public void HandleWaveStart(WaveDefinition waveDefinition)
    {
        waveManager.StartCoroutine(waveManager.StartWave());
        CurrentWaveNumber = waveManager.CurrentWaveIndex; 
        OnUpdateWaveNum?.Invoke(CurrentWaveNumber);
        OnWaveStarted?.Invoke(waveDefinition);
    }

    private void HandleWaveCompleted(WaveDefinition completedWave)
    {
        OnWaveEnded?.Invoke(completedWave);
        HandleIntermissionStart(completedWave);


    }


    // ===== Intermission =====
    public void HandleIntermissionStart(WaveDefinition completedWave)
    {
        OnIntermissionStarted?.Invoke(completedWave);
    }

    public void HandleIntermissionComplete()
    {
        HandleWaveStart(waveManager.Waves[waveManager.CurrentWaveIndex]);
    }
}
