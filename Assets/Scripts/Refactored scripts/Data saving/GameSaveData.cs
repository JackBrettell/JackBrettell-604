using System.Collections.Generic;

[System.Serializable]
public class WeaponUpgradeData
{
    public string WeaponName;
    public float Damage;
    public float FireRate;
    public int AmmoCapacity;

    public int DamageUpgradeCount;
    public float FireRateUpgradeCount;
    public int AmmoUpgradeCount;

    public WeaponUpgradeData(string name, float damage, float fireRate, int ammo, int damageCount, float fireCount, int ammoCount)
    {
        WeaponName = name;
        this.Damage = damage;
        this.FireRate = fireRate;
        this.AmmoCapacity = ammo;

        this.DamageUpgradeCount = damageCount;
        this.FireRateUpgradeCount = fireCount;
        this.AmmoUpgradeCount = ammoCount;

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
