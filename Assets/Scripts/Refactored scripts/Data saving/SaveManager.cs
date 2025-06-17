using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private MoneyManager moneyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            DeleteSavedData();
        }
    }

    public void DeleteSavedData()
    {
        SaveSystem.DeleteSaveData();
    }
}
