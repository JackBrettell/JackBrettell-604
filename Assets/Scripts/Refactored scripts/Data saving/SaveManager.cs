using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private MoneyManager moneyManager;

    private void Start()
    {
        GameSaveData data = SaveSystem.LoadData();
        if (data != null)
        {
            waveManager.SetWaveIndex(data.currentWave);
            moneyManager.SetMoney(data.currentMoney);
            waveManager.StartNextWave();

        }
        else
        {
            waveManager.SetWaveIndex(0);
            waveManager.StartNextWave();
            Debug.LogWarning("(Save Manager) No saved data found, starting from the beginning.");
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void SaveProgress()
    {
        int currentWave = waveManager.CurrentWaveIndex;
        int currentMoney = moneyManager.CurrentMoney;  
        SaveSystem.SaveGame(new GameSaveData(currentWave, currentMoney));
        Debug.Log($"[GameMediator] Saved wave {currentWave} and money {currentMoney}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            DeleteSavedData();
        }
    }

    public static void DeleteSavedData()
    {
        SaveSystem.DeleteSaveData();
        Debug.Log("[SaveManager] Save data deleted.");
    }
}
