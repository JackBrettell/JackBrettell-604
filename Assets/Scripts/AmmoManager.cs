using System;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private int ammoCapacity;
    private int currentAmmo;
    public Action<int, int> OnAmmoChanged;

    public void Initialize(GunBehaviourBase gun)
    {
        ammoCapacity = gun.AmmoCapacity;
        currentAmmo = ammoCapacity;

    }

    public int CurrentAmmo => currentAmmo;

    public void ReduceAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            OnAmmoChanged?.Invoke(currentAmmo, ammoCapacity);
        }
    }

    public void Reload()
    {
        currentAmmo = ammoCapacity;
        OnAmmoChanged?.Invoke(currentAmmo, ammoCapacity);
    }

    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo = Mathf.Clamp(ammo, 0, ammoCapacity);
        OnAmmoChanged?.Invoke(currentAmmo, ammoCapacity);
    }


}