using System.Collections.Generic;
using System.Linq;
using UnityEditor.Overlays;
using UnityEngine;
using static GameSaveData;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private WeaponUpgradeManager weaponUpgradeManager;
    // [SerializeField] private WeaponList weaponList;
    private GunBehaviour[] gunBehaviours;

private void Start()
{
    gunBehaviours = FindObjectsOfType<GunBehaviour>(true);

    GameSaveData data = SaveSystem.LoadData();
    if (data != null)
    {
        waveManager.SetWaveIndex(data.CurrentWave);
        moneyManager.SetMoney(data.CurrentMoney);

        // Sync unlocked state to both stats and gun behaviour
        foreach (var gun in gunBehaviours)
        {
            if (gun.weaponStats == null) continue;

            bool isUnlocked = data.UnlockedWeapons.Contains(gun.weaponStats.weaponName);
            gun.weaponStats.isUnlocked = isUnlocked;
            gun.isWeaponUnlocked = isUnlocked;
        }

        waveManager.StartNextWave();
    }
    else
    {
        waveManager.SetWaveIndex(0);
        waveManager.StartNextWave();
        Debug.LogWarning("(SaveManager) No saved data found, starting from the beginning.");
    }
}

    public void SaveProgress()
    {
        int currentWave = waveManager.CurrentWaveIndex;
        int currentMoney = moneyManager.CurrentMoney;

        List<string> unlockedWeapons = new List<string>();

        foreach (var weapon in gunBehaviours)
        {
            if (weapon.isWeaponUnlocked)
            {
                unlockedWeapons.Add(weapon.weaponStats.weaponName);
            }
        }

        GameSaveData saveData = new GameSaveData(currentWave, currentMoney, unlockedWeapons.ToArray());

        SaveSystem.SaveGame(saveData);
        Debug.Log("[SaveManager] Progress saved.");
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
