using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public GunBehaviour currentGun;
    public GunBehaviour[] gunBehaviour;
    public AmmoManager ammoManager;
    public HUD hud;

    // Store ammo count
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

        weaponAmmoMap[currentGun] = currentGun.ammoManager.CurrentAmmo; // Save ammo count
       

        foreach (var gun in gunBehaviour)
        {
            gun.gameObject.SetActive(false);
        }

        currentGun = gunBehaviour[weaponIndex];
        currentGun.gameObject.SetActive(true);

        // Preserve ammo count on weapon switch 
        if (weaponAmmoMap.TryGetValue(currentGun, out int savedAmmo))
        {
            currentGun.ammoManager.Initialize(savedAmmo);
        }
        else
        {
            currentGun.InitializeAmmo();
        }
    }
}
