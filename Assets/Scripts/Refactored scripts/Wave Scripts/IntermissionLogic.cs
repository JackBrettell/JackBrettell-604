using UnityEngine;
using System.Collections;
using System;

public class IntermissionLogic : MonoBehaviour
{
    [Header("Intermission")]
    [SerializeField] private WaveManager waveManager;

    public Action OnIntermissionComplete;
    public Action<float> OnUpdateIntermissionTime;
    public void StartIntermissionTimer(WaveDefinition waveInfo)
    {
        int duration = waveInfo.intermissionDuration;
        StartCoroutine(IntermissionCountdown(duration));
    }

    private IEnumerator IntermissionCountdown(int duration)
    {
        for (int timeLeft = duration; timeLeft >= 0; timeLeft--)
        {
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;

            yield return new WaitForSeconds(1f);

            OnUpdateIntermissionTime?.Invoke(timeLeft);
        }

        OnIntermissionComplete?.Invoke();
    }
}
