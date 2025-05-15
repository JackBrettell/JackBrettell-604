using UnityEngine;
using UnityEngine.EventSystems;

public class HUDMediator : MonoBehaviour
{
    //[SerializeField] private AmmoUI ammoUI;
    [SerializeField] private CrosshairUI crosshairUI;
    // [SerializeField] private WaveUI waveUI;
    [SerializeField] private IntermissionUI intermissionUI;
    public IntermissionUI IntermissionUI => intermissionUI;
    // [SerializeField] private HealthBarUI healthBarUI;
    //[SerializeField] private MoneyUI moneyUI;
    //[SerializeField] private HUDSway sway;

    [SerializeField] private AmmoManager ammoManager;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GunBehaviour gunBehaviour;
    [SerializeField] private WaveMediator waveMediator;

    private GunBullet[] bullets;

    private void Start()
    {
        // ammoUI.Initialize(ammoManager);
        // moneyUI.Initialize(moneyManager);
        // healthBarUI.Initialize(playerHealth);
        // waveUI.Initialize(waveManager);
    }

    private void OnEnable()
    {
        GunBullet.OnAnyBulletHit += crosshairUI.ShowHitMarker;
        gunBehaviour.OnWeaponFired += crosshairUI.crosshairScale;

        waveMediator.OnIntermissionStarted += intermissionUI.StartIntermissionTimer;
    }

    private void OnDisable()
    {
        GunBullet.OnAnyBulletHit -= crosshairUI.ShowHitMarker;
        gunBehaviour.OnWeaponFired -= crosshairUI.crosshairScale;

       // waveManager.OnWaveCompleted -= HandleWaveCompleted;
    }





}
