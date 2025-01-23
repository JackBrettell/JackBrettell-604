using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using JetBrains.Annotations;

public class WeaponBase : MonoBehaviour
{
    private WeaponManager weaponManager;
    
    // Pistol
    private PistolBehaviour pistolBehaviour;

    // Rifle
    private RifleBehaviour rifleBehaviour;
    public int rifleAmmoCount; //Temp
    public bool isRifleRelaoding = false;
    private bool isFiring = false; // Tracks if the fire button is held
    private Coroutine firingCoroutine;



    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        rifleBehaviour = GetComponent<RifleBehaviour>();
        pistolBehaviour = GetComponent<PistolBehaviour>();

       

    }


    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (weaponManager.isRifleEquiped && !isRifleRelaoding)
            {
                isRifleRelaoding = true;
                rifleBehaviour.ReloadingSequence();
            }
        }
    }















    public void OnFire(InputAction.CallbackContext context)
    {
        if (!isRifleRelaoding)
        {

            if (context.started || context.performed && weaponManager.isRifleEquiped && rifleAmmoCount > 0)
            {
                if (!isFiring)
                {
                    isFiring = true;
                    firingCoroutine = StartCoroutine(AutoFireRifle());
                }
            }
            else if (context.canceled) // Button released
            {
                isFiring = false;

                if (firingCoroutine != null)
                {
                    StopCoroutine(firingCoroutine);
                }
            }
        }

    }
    private IEnumerator AutoFireRifle()
    {
        while (isFiring && rifleAmmoCount > 0)
        {
            FireRifle(); 
            yield return new WaitForSeconds(1f / rifleBehaviour.fireRate); 
        }
    }

    public void FireRifle()
    {
        if (rifleAmmoCount > 0)
        {
            rifleAmmoCount--;
            rifleBehaviour.FiringSequence();
        }
        else
        {
            Debug.Log("Out of ammo!");
            isFiring = false; 
        }
    }
}
