using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using JetBrains.Annotations;

public class WeaponBase : MonoBehaviour
{
    private WeaponManager weaponManager;
    private Crosshair crosshair;



    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        crosshair = GetComponent<Crosshair>();
    }


    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            weaponManager.currentGun.ReloadingSequence();
            crosshair.crosshairScale();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // Handle rifle firing logic
        if (weaponManager.currentGun is RifleBehaviour)
        {
            if (context.started || context.performed) // Button pressed or held
            {
                weaponManager.currentGun.Fire();

            }
            else if (context.canceled)
            {
                weaponManager.currentGun.StopFire();

            }
        }
        // Handle pistol firing logic
        else if (weaponManager.currentGun is PistolBehaviour)
        {
            if (context.performed) // Button pressed or held
            {
                weaponManager.currentGun.Fire();

                crosshair.crosshairScale();

            }
        }
    }




}
