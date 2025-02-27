using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using JetBrains.Annotations;

public class WeaponInputManager : MonoBehaviour
{
   [SerializeField] private HUD HUD;
   [SerializeField] private WeaponManager weaponManager;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponManager.currentGun.ReloadingSequence();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (StoreMenus.IsShopOpen) return; // Prevent firing when shop is open

        if (context.started || context.performed)
        {
            weaponManager.currentGun.Fire();
        }
        else if (context.canceled)
        {
            weaponManager.currentGun.StopFire();
        }

    }

}


