using TMPro;
using UnityEngine;

public class WeaponUpgrades : MonoBehaviour
{
    private WeaponStats currentWeapon;

    [Header("UI")]
    public TextMeshProUGUI weaponNameText;

    public void Initialize(WeaponStats weapon)
    {
        currentWeapon = weapon;
        weaponNameText.text = $"Upgrading: {weapon.weaponName}";
    }

    public void UpgradeDamage()
    {
        currentWeapon.damage += 10;
        Debug.Log($"{currentWeapon.weaponName} damage upgraded to {currentWeapon.damage}");
    }

    public void UpgradeFireRate()
    {
        currentWeapon.fireRate += 1f;
        Debug.Log($"{currentWeapon.weaponName} fire rate upgraded to {currentWeapon.fireRate}");
    }

    public void UpgradeAmmoCapacity()
    {
        currentWeapon.ammoCapacity += 5;
        Debug.Log($"{currentWeapon.weaponName} ammo upgraded to {currentWeapon.ammoCapacity}");
    }
}
