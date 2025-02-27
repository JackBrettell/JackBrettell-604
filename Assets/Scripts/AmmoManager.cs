using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private WeaponStats weaponStats;  // Store current weapon's stats
    private int currentAmmo;

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => weaponStats != null ? weaponStats.ammoCapacity : 0;  // Get from WeaponStats

    public void Initialize(WeaponStats newWeaponStats)
    {
        weaponStats = newWeaponStats; 
        currentAmmo = weaponStats.ammoCapacity;
    }

    public void Reload()
    {
        if (weaponStats != null)
            currentAmmo = weaponStats.ammoCapacity;
    }

    public void ReduceAmmo()
    {
        if (currentAmmo > 0)
            currentAmmo--;
    }
}
