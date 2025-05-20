using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
    public GunBehaviour currentGun;
    [SerializeField] private WeaponInputManager weaponInputManager;
    public GunBehaviour[] gunBehaviour;
    public HUD hud;

    [Header("Grenade settings")]
    [SerializeField] private float grenadeCooldown = 1f;
    public float GrenadeCooldown => grenadeCooldown;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Transform grenadeSpawnPoint;
    [SerializeField] private float grenadeThrowForce = 10f;
    private bool isGrenadeOnCooldown = false;
    public Action OnThrowGrenade;
    public Action OnGrenadeFailed;
    public Action<float, float> OnGrenadeCooldownStarted;

    public event Action<GunBehaviour> OnWeaponSwitched;

    private Dictionary<GunBehaviour, int> weaponAmmoMap = new Dictionary<GunBehaviour, int>();

    private void OnEnable()
    {
        weaponInputManager.OnGrenadePressed += TryThrowGrenade;
    }
    private void OnDisable()
    {
        weaponInputManager.OnGrenadePressed -= TryThrowGrenade;
    }

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
    private void TryThrowGrenade()
    {
        if (isGrenadeOnCooldown)
        {
            OnGrenadeFailed?.Invoke();
            return;
        }
        else
        {

            StartCoroutine(GrenadeCooldownTimer());

            OnThrowGrenade?.Invoke();
            Instantiate(grenadePrefab, grenadeSpawnPoint.position, grenadeSpawnPoint.rotation)
            .GetComponent<Rigidbody>()
            .AddForce(grenadeSpawnPoint.forward * grenadeThrowForce, ForceMode.Impulse);
        }

    }

private IEnumerator GrenadeCooldownTimer()
{
    isGrenadeOnCooldown = true;

    float elapsed = 0f;
    while (elapsed < grenadeCooldown)
    {
        OnGrenadeCooldownStarted?.Invoke(grenadeCooldown - elapsed, grenadeCooldown);
        yield return null;
        elapsed += Time.deltaTime;
    }

    isGrenadeOnCooldown = false;
    OnGrenadeCooldownStarted?.Invoke(0f,grenadeCooldown); // final update
}



}
