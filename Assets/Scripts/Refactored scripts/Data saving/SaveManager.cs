using System;
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
    private GunBehaviourBase[] gunBehaviours;

private void Start()
{

        gunBehaviours = FindObjectsOfType<GunBehaviourBase>();

        GameSaveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            waveManager.SetWaveIndex(data.CurrentWave);
            moneyManager.SetMoney(data.CurrentMoney);

            foreach (var gun in gunBehaviours)
            {
                if (gun.weaponStats == null) continue;

                bool isUnlocked = data.UnlockedWeapons.Contains(gun.weaponStats.weaponName);
                gun.weaponStats.isUnlocked = isUnlocked;
                gun.isWeaponUnlocked = isUnlocked;

                // Find upgrade data for this weapon
                if (isUnlocked && data.OnUpgrades != null)
                {
                    WeaponUpgradeData upgrade = Array.Find(data.OnUpgrades, w => w.WeaponName == gun.weaponStats.weaponName);
                    if (upgrade != null)
                    {
                        var upgradeCosts = weaponUpgradeManager.UpgradeCosts;

                        for (int i = 0; i < upgrade.DamageUpgradeCount; i++)
                            gun.IncreaseDamage(upgradeCosts.damageUpgradeAmount);

                        for (int i = 0; i < (int)upgrade.FireRateUpgradeCount; i++)
                            gun.IncreaseFireRate(upgradeCosts.fireRateUpgradeAmount);

                        for (int i = 0; i < upgrade.AmmoUpgradeCount; i++)
                            gun.IncreaseAmmoCapacity(upgradeCosts.ammoUpgradeAmount);

                    }
                }
            }

        }

        waveManager.StartNextWave();

    }

    public void SaveProgress()
    {
        int currentWave = waveManager.CurrentWaveIndex;
        int currentMoney = moneyManager.CurrentMoney;

        List<string> unlockedWeapons = new List<string>();
        List<WeaponUpgradeData> upgradeDataList = new List<WeaponUpgradeData>();

        foreach (var weapon in gunBehaviours)
        {
            if (weapon.isWeaponUnlocked)
            {
                unlockedWeapons.Add(weapon.weaponStats.weaponName);

                WeaponUpgradeData upgradeData = new WeaponUpgradeData(
                    weapon.weaponStats.weaponName,
                    weapon.Damage,
                    weapon.weaponStats.fireRate,
                    weapon.AmmoCapacity,
                    weapon.DamageUpgradeAmount,
                    weapon.FireRateUpgradeAmount,
                    weapon.AmmoUpgradeAmount
                );

                upgradeDataList.Add(upgradeData);
            }
        }

        GameSaveData saveData = new GameSaveData(currentWave, currentMoney, unlockedWeapons.ToArray())
        {
            OnUpgrades = upgradeDataList.ToArray()
        };

        SaveSystem.SaveGame(saveData);
        Debug.Log("[SaveManager] Progress saved.");
    }



    // Update is called once per frame
    void Update()
    {
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F1))
            {
                DeleteSavedData();
            }
#endif
        }
    }

    public static void DeleteSavedData()
    {
        SaveSystem.DeleteSaveData();
        Debug.Log("[SaveManager] Save data deleted.");
    }
}
