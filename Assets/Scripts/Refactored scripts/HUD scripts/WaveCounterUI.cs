using UnityEngine;
using TMPro;

public class WaveCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveCounterText;

    public void UpdateWaveCounter(int waveNumber)
    {
      /*  if (waveNumber == 0)
        {
            waveCounterText.text = "Wave 1"; // Display starts at Wave 1
        }
        else*/
       // {
            waveCounterText.text = $"Wave {waveNumber + 1}"; // Add 1 so wave count is accurate
      //  }

    }

}
