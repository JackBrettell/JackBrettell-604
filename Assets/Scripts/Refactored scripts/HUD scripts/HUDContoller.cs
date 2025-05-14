using UnityEngine;
using UnityEngine.EventSystems;

public class HUDMediator : MonoBehaviour
{
    //[SerializeField] private AmmoUI ammoUI;
    [SerializeField] private CrosshairUI crosshairUI;
   // [SerializeField] private WaveUI waveUI;
    [SerializeField] private IntermissionUI intermissionUI;
   // [SerializeField] private HealthBarUI healthBarUI;
    //[SerializeField] private MoneyUI moneyUI;
    //[SerializeField] private HUDSway sway;

    [SerializeField] private AmmoManager ammoManager;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GunBehaviour gunBehaviour;

    private void Start()
    {
        // ammoUI.Initialize(ammoManager);
        // moneyUI.Initialize(moneyManager);
        // healthBarUI.Initialize(playerHealth);
        // waveUI.Initialize(waveManager);

        gunBehaviour.OnWeaponFired += crosshairUI.crosshairScale;
    }
}
