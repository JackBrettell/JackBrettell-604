using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public GunBehaviour currentGun;
    public GunBehaviour[] gunBehaviour;
    public HUD hud;

    private Dictionary<GunBehaviour, int> weaponAmmoMap = new Dictionary<GunBehaviour, int>();

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

        // Initialize ammo using the weapon's assigned `WeaponStats`
        if (currentGun.weaponStats != null)
        {
            if (weaponAmmoMap.TryGetValue(currentGun, out int savedAmmo))
            {
                currentGun.ammoManager.Initialize(currentGun.weaponStats);
                for (int i = 0; i < (currentGun.weaponStats.ammoCapacity - savedAmmo); i++)
                {
                    currentGun.ammoManager.ReduceAmmo();
                }
            }
            else
            {
                currentGun.ammoManager.Initialize(currentGun.weaponStats);
            }
        }
        else
        {
            Debug.LogError($"{currentGun.gameObject.name}: Missing WeaponStats! Assign it in the Inspector.");
        }
    }
}
