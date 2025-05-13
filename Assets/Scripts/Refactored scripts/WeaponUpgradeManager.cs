using UnityEngine;
using System;

public class WeaponUpgradeManager : MonoBehaviour
{
    public event Action OnDamageUpgradeSuccess;
    public event Action OnFireRateUpgradeSuccess;
    public event Action OnAmmoUpgradeSuccess;
    public event Action OnUpgradeFailed;
    public event Action<GunBehaviour[], WeaponStats[] > OnPopulateWeaponButtons;
    public event Action<UpgradeCostsAndAmounts> OnUpgradeCostsAndAmountsChanged;

    [SerializeField] private UpgradeCostsAndAmounts upgradeCostsAndAmounts;

    private GunBehaviour currentGun;
    [SerializeField] private MoneyManager moneyManager;

    [SerializeField] private GunBehaviour[] gunBehaviourList;
    [SerializeField] private WeaponStats[] weaponStatsList;



    void Start()
    {
        Debug.Log("Start");
        OnPopulateWeaponButtons?.Invoke(gunBehaviourList, weaponStatsList);
        OnUpgradeCostsAndAmountsChanged?.Invoke(upgradeCostsAndAmounts);
    }



    public void GunChanged(GunBehaviour gun)
    {
        currentGun = gun;
        Debug.Log("GunChanged");
    }

    public void TryUpgradeDamage()
    {
        if (moneyManager.RemoveMoney(upgradeCostsAndAmounts.damageUpgradeCost))
        {
            currentGun.IncreaseDamage(upgradeCostsAndAmounts.damageUpgradeAmount);
            OnDamageUpgradeSuccess?.Invoke();
        }
        else OnUpgradeFailed?.Invoke();
    }

    public void TryUpgradeFireRate()
    {
        if (moneyManager.RemoveMoney(upgradeCostsAndAmounts.fireRateUpgradeCost))
        {
            currentGun.IncreaseFireRate(upgradeCostsAndAmounts.fireRateUpgradeAmount);
            OnFireRateUpgradeSuccess?.Invoke();
        }
        else OnUpgradeFailed?.Invoke();
    }

    public void TryUpgradeAmmo()
    {
        if (moneyManager.RemoveMoney(upgradeCostsAndAmounts.ammoUpgradeCost))
        {
            currentGun.IncreaseAmmoCapacity(upgradeCostsAndAmounts.ammoUpgradeAmount);
            OnAmmoUpgradeSuccess?.Invoke();
        }
        else OnUpgradeFailed?.Invoke();
    }


}

[Serializable]
public class UpgradeCostsAndAmounts
{
     public int damageUpgradeCost = 10;
     public int damageUpgradeAmount = 10;

     public int fireRateUpgradeCost = 10;
     public int fireRateUpgradeAmount = 10;

     public int ammoUpgradeCost = 10;
     public int ammoUpgradeAmount = 10;
}