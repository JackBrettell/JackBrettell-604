using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class WeaponManager : MonoBehaviour
{
    public GunBehaviour currentGun;
    public GunBehaviour[] gunBehaviour;
    public HUD hud;

    public event Action<GunBehaviour> OnWeaponSwitched;

    private Dictionary<GunBehaviour, int> weaponAmmoMap = new Dictionary<GunBehaviour, int>();

    private void Start()
    {
        SwitchToWeapon(0); // Start with the first weapon (Pistol)
    }

    public void OnWeaponSwitch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (int.TryParse(context.control.name, out int weaponChoice))
            {
                SwitchToWeapon(weaponChoice - 1);
                hud.updateAmmoCount();
            }
        }
    }



    private void SwitchToWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= gunBehaviour.Length) return;

        // Check if the weapon is unlocked
        GunBehaviour selectedGun = gunBehaviour[weaponIndex];
        if (!selectedGun.isWeaponUnlocked)
        {
            return; 
        }

        // Save current weapon's ammo before switching
        if (currentGun != null)
            weaponAmmoMap[currentGun] = currentGun.ammoManager.CurrentAmmo;

        // Deactivate all weapons
        foreach (var gun in gunBehaviour)
        {
            gun.gameObject.SetActive(false);
        }

        // Switch to new weapon
        currentGun = gunBehaviour[weaponIndex];
        currentGun.gameObject.SetActive(true);

        // Initialize ammo manager 
        currentGun.ammoManager.Initialize(currentGun);

        // Restore saved ammo if available
        if (weaponAmmoMap.TryGetValue(currentGun, out int savedAmmo))
        {
            // Set the current ammo
            currentGun.ammoManager.SetCurrentAmmo(savedAmmo);
        }
        else
        {
            currentGun.ammoManager.Reload();
        }

        OnWeaponSwitched?.Invoke(currentGun);

    }
}
