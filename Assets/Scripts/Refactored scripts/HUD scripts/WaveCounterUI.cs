using UnityEngine;
using TMPro;

public class WaveCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveCounterText;

    private void Start()
    {
        waveCounterText.text = "Wave 1"; // Display starts at Wave 1

    }

    public void UpdateWaveCounter(int waveNumber)
    {
        waveCounterText.text = $"Wave {waveNumber + 2}"; // Just add 2 so wave count is accurate
    }

}
