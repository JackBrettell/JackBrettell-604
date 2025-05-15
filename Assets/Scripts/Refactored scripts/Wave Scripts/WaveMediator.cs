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

    private void HandleIntermissionComplete()
    {
        waveManager.StartWave();
    }
}
