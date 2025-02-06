using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using JetBrains.Annotations;

public class WeaponBase : MonoBehaviour
{
    private WeaponManager weaponManager;
    private HUD crosshair;



    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        crosshair = GetComponent<HUD>();
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
        if (context.started || context.performed)
        {
            weaponManager.currentGun.Fire();
            crosshair.crosshairScale();
        }
        else if (context.canceled)
        {
            weaponManager.currentGun.StopFire();
        }

    }

}


