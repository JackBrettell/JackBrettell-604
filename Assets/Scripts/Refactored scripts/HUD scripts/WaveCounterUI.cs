using UnityEngine;
using TMPro;
public class WaveCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveCounterText;

    private void Start()
    {
        waveCounterText.text = "Wave 1"; 
    }
    public void UpdateWaveCounter(int waveNumber)
    {
        Debug.Log("AAAAAAA: {waveNumber}");
        waveCounterText.text = $"Wave {waveNumber + 1}"; // Add 1 so first wave is displayed as Wave 1
    }
}
