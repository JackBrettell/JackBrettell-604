using System;
using UnityEngine;

public class WaveMediator : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private HUDMediator hudMediator;
    [SerializeField] private IntermissionLogic intermissionLogic;
    [SerializeField] private SaveManager saveManager;


    private void OnEnable()
    {
        waveManager.OnWaveCompleted += HandleWaveCompleted;
        intermissionLogic.OnIntermissionComplete += HandleIntermissionComplete;

    }

    private void OnDisable()
    {
        waveManager.OnWaveCompleted -= HandleWaveCompleted;
        intermissionLogic.OnIntermissionComplete -= HandleIntermissionComplete;

    }

    // ===== Wave =====

    private void HandleWaveCompleted(WaveDefinition completedWave, int currentWaveIndex)
    {

        intermissionLogic.StartIntermissionTimer(completedWave);
        saveManager.SaveProgress(); 

    }


    // ===== Intermission =====
    public void HandleIntermissionStart(WaveDefinition completedWave)
    {
    }

    public void HandleIntermissionComplete()
    {
        waveManager.StartNextWave();
    }
}
