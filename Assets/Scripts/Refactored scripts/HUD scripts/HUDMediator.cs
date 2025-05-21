using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUDMediator : MonoBehaviour
{
    //[SerializeField] private AmmoUI ammoUI;
    [SerializeField] private CrosshairUI crosshairUI;
    // [SerializeField] private WaveUI waveUI;
    [SerializeField] private IntermissionUI intermissionUI;

    public IntermissionUI IntermissionUI => intermissionUI;
    [SerializeField] private WaveCounterUI waveCounterUI;

    // [SerializeField] private HealthBarUI healthBarUI;
    //[SerializeField] private MoneyUI moneyUI;
    //[SerializeField] private HUDSway sway;

    [SerializeField] private AmmoManager ammoManager;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GunBehaviour gunBehaviour;
    [SerializeField] private IntermissionLogic intermissionLogic;
    [SerializeField] private HealthBarUI healthBarUI;
    [SerializeField] private DamageOverlay damageOverlay;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GrenadeCooldownUI grenadeCooldownUI;
    [SerializeField] private AmmoCountUI ammoCountUI;
    private GunBullet[] bullets;


    // ==== Events ====
    public Action OnIntermissionCompleted;


    private void Start()
    {
        // ammoUI.Initialize(ammoManager);
        // moneyUI.Initialize(moneyManager);
        // healthBarUI.Initialize(playerHealth);
        // waveUI.Initialize(waveManager);
    }

    private void OnEnable()
    {
        //Hitmarker
        GunBullet.OnAnyBulletHit += crosshairUI.ShowHitMarker;

        //Crosshair recoil
        gunBehaviour.OnWeaponFired += crosshairUI.crosshairScale;

        //Intermission
        intermissionLogic.OnUpdateIntermissionTime += IntermissionUI.StartIntermissionTimer;
        intermissionLogic.OnIntermissionComplete += IntermissionUI.IntermissionCompleted;

        //Health
        playerHealth.OnHealthChanged += healthBarUI.UpdateHealthBar;
        playerHealth.OnInitialiseHealthbar += healthBarUI.InitialiseHealthUI;
        playerHealth.OnUpdateHealthOverlay += damageOverlay.UpdateDamageOverlay;

        //Grenade
        weaponManager.OnGrenadeCooldownStarted += grenadeCooldownUI.UpdateCoolDownBar;
        weaponManager.OnGrenadeFailed += grenadeCooldownUI.AnimateFailIcon;

        //Ammo
        ammoManager.OnAmmoChanged += ammoCountUI.UpdateAmmoBar;   


    }

    private void OnDisable()
    {
        GunBullet.OnAnyBulletHit -= crosshairUI.ShowHitMarker;
        gunBehaviour.OnWeaponFired -= crosshairUI.crosshairScale;
        intermissionLogic.OnUpdateIntermissionTime -= IntermissionUI.StartIntermissionTimer;
        intermissionLogic.OnIntermissionComplete -= IntermissionUI.IntermissionCompleted;

        //Health
        playerHealth.OnHealthChanged -= healthBarUI.UpdateHealthBar;
        playerHealth.OnInitialiseHealthbar -= healthBarUI.InitialiseHealthUI;

        //Grenade
        weaponManager.OnGrenadeCooldownStarted -= grenadeCooldownUI.UpdateCoolDownBar;
        weaponManager.OnGrenadeFailed -= grenadeCooldownUI.AnimateFailIcon;
    }
}
