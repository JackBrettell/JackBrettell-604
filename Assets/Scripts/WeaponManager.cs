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
                SwitchToWeapon(weaponChoice -1); 
            }
        }
    }

    private void SwitchToWeapon(int weaponIndex)
    {
        if (weaponIndex == 1)
        {

            gunBehaviour[1].gameObject.SetActive(false);
            gunBehaviour[0].gameObject.SetActive(true);

            currentGun = gunBehaviour[0];
        }
        else if (weaponIndex == 2) 
        {

            gunBehaviour[1].gameObject.SetActive(true);
            gunBehaviour[0].gameObject.SetActive(false);

            currentGun = gunBehaviour[1];

        }
    }
}
