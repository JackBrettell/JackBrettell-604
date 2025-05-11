using TMPro;
using UnityEngine;

public class WeaponUpgrades : MonoBehaviour
{
    private WeaponStats currentWeapon;
    private GunBehaviour currentGun;
    [SerializeField] private MoneyManager moneyManager;

    [Header("Unlock gun")]
    [SerializeField] private bool isUnlocked = false;

    [Header("Damage upgrade")]
    [SerializeField] private int damageUpgradeCost = 10;
    [SerializeField] private int damageUpgradeAmount = 10;
    [SerializeField] private TMPro.TextMeshProUGUI damageUpgradeCostText;

    [Header("Damage upgrade")]
    [SerializeField] private int fireRateUpgradeCost = 10;
    [SerializeField] private int fireRateUpgradeAmount = 10;
    [SerializeField] private TMPro.TextMeshProUGUI fireRateUpgradeCostText;

    [Header("Ammo upgrade")]
    [SerializeField] private int increaseAmmoCapacityCost = 10;
    [SerializeField] private int increaseAmmoCapacityAmmount = 10;
    [SerializeField] private TMPro.TextMeshProUGUI increaseAmmoCapacityCostText;

    [Header("UI")]
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI currentMoneyText;
    [HideInInspector] public int currentMoney;

    public void Initialize(WeaponStats weapon, GunBehaviour gun)
    {
        currentWeapon = weapon;
        currentGun = gun;
        weaponNameText.text = $"Upgrading: {weapon.weaponName}";

        // Assign the cost texts
        damageUpgradeCostText.text = damageUpgradeCost.ToString();
        fireRateUpgradeCostText.text = fireRateUpgradeCost.ToString();
        increaseAmmoCapacityCostText.text = increaseAmmoCapacityCost.ToString();

    }

    public void UpdateCurrentMoney()
    { 
        
    }



    public void UpgradeDamage()
    {
        if (moneyManager.RemoveMoney(damageUpgradeCost))
        {
            currentGun.IncreaseDamage(damageUpgradeAmount);
        }
        else
        {
            // Brokie logic
        }
    }


    public void UpgradeFireRate()
    {
        if (moneyManager.RemoveMoney(fireRateUpgradeCost))
        {
            currentGun.IncreaseFireRate(fireRateUpgradeAmount);
        }
        else
        {
            // Brokie logic
        }
    }

    public void UpgradeAmmoCapacity()
    {
        if (moneyManager.RemoveMoney(increaseAmmoCapacityCost))
        {
            currentGun.IncreaseAmmoCapacity(increaseAmmoCapacityAmmount);
        }
        else
        {
            // Brokie logic
        }
    }
}
