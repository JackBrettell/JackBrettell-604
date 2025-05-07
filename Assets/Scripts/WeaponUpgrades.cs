using TMPro;
using UnityEngine;

public class WeaponUpgrades : MonoBehaviour
{
    private WeaponStats currentWeapon;
    private GunBehaviour currentGun;
    [Header("Unlock gun")]
    [SerializeField] private bool isUnlocked = false;

    [Header("Damage upgrade")]
    [SerializeField] private int damageUpgradeCost = 10;
    [SerializeField] private int damageUpgradeAmount = 10;

    [Header("Damage upgrade")]
    [SerializeField] private int fireRateUpgradeCost = 10;
    [SerializeField] private int fireRateUpgradeAmount = 10;

    [Header("Ammo upgrade")]
    [SerializeField] private int increaseAmmoCapacityCost = 10;
    [SerializeField] private int increaseAmmoCapacityAmmount = 10;

    [Header("UI")]
    public TextMeshProUGUI weaponNameText;

    public void Initialize(WeaponStats weapon, GunBehaviour gun)
    {
        currentWeapon = weapon;
        currentGun = gun;
        weaponNameText.text = $"Upgrading: {weapon.weaponName}";
    }


    public void UpgradeDamage()
    {
        currentGun.IncreaseDamage(damageUpgradeAmount);
    }


    public void UpgradeFireRate()
    {
        currentGun.IncreaseFireRate(fireRateUpgradeAmount);
    }

    public void UpgradeAmmoCapacity()
    {
        currentGun.IncreaseAmmoCapacity(increaseAmmoCapacityAmmount);
    }
}
