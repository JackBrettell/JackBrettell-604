using UnityEngine;
using DG;
using UnityEngine.InputSystem;
using DG.Tweening;

public class WeaponManager : MonoBehaviour
{
    public GunBehaviour currentGun;
    public GunBehaviour[] gunBehaviour;

    public void OnWeaponSwitch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var control = context.control;

            if (int.TryParse(control.name, out int weaponChoice))
            {
                SwitchToWeapon(weaponChoice - 1); 
            }
        }
    }

    private void SwitchToWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= gunBehaviour.Length)
        {
            return; 
        }

        // Disable all weapons
        foreach (var gun in gunBehaviour)
        {
            gun.gameObject.SetActive(false);
        }

        // Enable selected weapon
        gunBehaviour[weaponIndex].gameObject.SetActive(true);
        currentGun = gunBehaviour[weaponIndex];
    }
}
