using System.Collections.Generic;

[System.Serializable]
public class WeaponUpgradeData
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public int ammoCapacity;

    public WeaponUpgradeData(string name, float damage, float fireRate, int ammo)
    {
        weaponName = name;
        this.damage = damage;
        this.fireRate = fireRate;
        this.ammoCapacity = ammo;
    }
}

[System.Serializable]
public class GameSaveData
{
    public int CurrentWave;
    public int CurrentMoney;
    public string[] UnlockedWeapons;
    public WeaponUpgradeData[] OnUpgrades;

    public GameSaveData(int waveIndex, int money, string[] unlockedWeapons)
    {
        CurrentWave = waveIndex;
        CurrentMoney = money;
        UnlockedWeapons = unlockedWeapons;
    }
}
