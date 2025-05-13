using UnityEngine;

public class WeaponUpgradeMediator : MonoBehaviour
{
    [SerializeField] private WeaponUpgradeManager upgradeManager;
    [SerializeField] private WeaponUpgradeView upgradeView;
    [SerializeField] private AmmoManager ammoManager;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private MoneyManager moneyManager;


    private void OnEnable()
    {
        // Forward UI events to manager
        upgradeView.OnDamageUpgradeClicked += upgradeManager.TryUpgradeDamage;
        upgradeView.OnFireRateUpgradeClicked += upgradeManager.TryUpgradeFireRate;
        upgradeView.OnAmmoUpgradeClicked += upgradeManager.TryUpgradeAmmo;


        // Forward manager events to view
        upgradeManager.OnDamageUpgradeSuccess += upgradeView.AnimateDamageSuccess;
        upgradeManager.OnFireRateUpgradeSuccess += upgradeView.AnimateFireRateSuccess;
        upgradeManager.OnAmmoUpgradeSuccess += () =>
        {
            //ammoManager.Initialize(gun);
            //upgradeView.AnimateAmmoSuccess();
        };
        upgradeManager.OnUpgradeFailed += upgradeView.AnimateUpgradeFailure;
        upgradeManager.OnPopulateWeaponButtons += upgradeView.PopulateWeaponButtons;

        upgradeManager.OnUpgradeCostsAndAmountsChanged += upgradeView.Initialize;



        weaponManager.OnWeaponSwitched += (gun) =>
        {
            upgradeManager.GunChanged(gun);
        };

        moneyManager.OnMoneyChanged += (amount) =>
        {
            upgradeView.UpdateMoney(amount);
        };
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        upgradeView.OnDamageUpgradeClicked -= upgradeManager.TryUpgradeDamage;
        upgradeView.OnFireRateUpgradeClicked -= upgradeManager.TryUpgradeFireRate;
        upgradeView.OnAmmoUpgradeClicked -= upgradeManager.TryUpgradeAmmo;
        upgradeManager.OnDamageUpgradeSuccess -= upgradeView.AnimateDamageSuccess;
        upgradeManager.OnFireRateUpgradeSuccess -= upgradeView.AnimateFireRateSuccess;
        upgradeManager.OnAmmoUpgradeSuccess -= () =>
        {
            //ammoManager.Initialize(gun);
            //upgradeView.AnimateAmmoSuccess();
        };
        upgradeManager.OnUpgradeFailed -= upgradeView.AnimateUpgradeFailure;
        upgradeManager.OnPopulateWeaponButtons -= upgradeView.PopulateWeaponButtons;

        upgradeManager.OnUpgradeCostsAndAmountsChanged -= upgradeView.Initialize;
        weaponManager.OnWeaponSwitched -= (gun) =>
        {
            upgradeManager.GunChanged(gun);
        };

        moneyManager.OnMoneyChanged -= (amount) =>
        {
            upgradeView.UpdateMoney(amount);
        };
    }
}


