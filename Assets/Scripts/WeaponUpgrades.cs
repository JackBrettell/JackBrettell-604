using TMPro;
using UnityEngine;
using DG.Tweening;

public class WeaponUpgrades : MonoBehaviour
{
    private WeaponStats currentWeapon;
    private GunBehaviour currentGun;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private AmmoManager ammoManager;

    [Header("Unlock gun")]
    [SerializeField] private bool isUnlocked = false;

    [Header("Damage upgrade")]
    [SerializeField] private int damageUpgradeCost = 10;
    [SerializeField] private int damageUpgradeAmount = 10;
    [SerializeField] private TMPro.TextMeshProUGUI damageUpgradeCostText;

    [Header("Damage upgrade")]
    [SerializeField] private int fireRateUpgradeCost = 10;
    [SerializeField] private int fireRateUpgradeAmount = 10;
    [SerializeField] private TMPro.TextMeshProUGUI fireRateUpgradeCostText;

    [Header("Ammo upgrade")]
    [SerializeField] private int increaseAmmoCapacityCost = 10;
    [SerializeField] private int increaseAmmoCapacityAmmount = 10;
    [SerializeField] private TMPro.TextMeshProUGUI increaseAmmoCapacityCostText;

    [Header("UI")]
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI currentMoneyText;
    [HideInInspector] public int currentMoney;

    public void Initialize(WeaponStats weapon, GunBehaviour gun)
    {
        currentWeapon = weapon;
        currentGun = gun;
        currentMoneyText.text = $"Money: £{MoneyManager.Instance.CurrentMoney}";

        // Assign the cost texts
        damageUpgradeCostText.text = $"£{damageUpgradeCost.ToString()}"; 
        fireRateUpgradeCostText.text = $"£{fireRateUpgradeCost.ToString()}";
        increaseAmmoCapacityCostText.text = $"£{increaseAmmoCapacityCost.ToString()}";

    }

    // Subscribe/unsubscribe to the money change event
    private void OnEnable()
    {
        MoneyManager.Instance.OnMoneyChanged += UpdateCurrentMoneyText;
    }

    private void OnDisable()
    {

        MoneyManager.Instance.OnMoneyChanged -= UpdateCurrentMoneyText;
    }

    private void UpdateCurrentMoneyText(int newMoney)
    {
        currentMoneyText.text = $"Money: £{newMoney}";
    }

    public void UpgradeDamage()
    {
        if (moneyManager.RemoveMoney(damageUpgradeCost))
        {
            currentGun.IncreaseDamage(damageUpgradeAmount);

            // Fade to green
            damageUpgradeCostText.DOColor(Color.green, 0.25f).OnComplete(() =>
            {
                damageUpgradeCostText.DOColor(Color.white, 0.25f);
            });
        }
        else
        {
            // Fade to red
            damageUpgradeCostText.DOColor(Color.red, 0.25f).OnComplete(() =>
            {
                damageUpgradeCostText.DOColor(Color.white, 0.25f);
            });


        }
    }


    public void UpgradeFireRate()
    {
        if (moneyManager.RemoveMoney(fireRateUpgradeCost))
        {
            currentGun.IncreaseFireRate(fireRateUpgradeAmount);
            // Fade to green
            fireRateUpgradeCostText.DOColor(Color.green, 0.25f).OnComplete(() =>
            {
                fireRateUpgradeCostText.DOColor(Color.white, 0.25f);
            });
        }
        else
        {
            // Fade to red
            fireRateUpgradeCostText.DOColor(Color.red, 0.25f).OnComplete(() =>
            {
                fireRateUpgradeCostText.DOColor(Color.white, 0.25f);
            });
        }
    }

    public void UpgradeAmmoCapacity()
    {
        if (moneyManager.RemoveMoney(increaseAmmoCapacityCost))
        {
            currentGun.IncreaseAmmoCapacity(increaseAmmoCapacityAmmount);

            ammoManager.Initialize(currentGun);

            // Fade to green
            increaseAmmoCapacityCostText.DOColor(Color.green, 0.25f).OnComplete(() =>
            {
                increaseAmmoCapacityCostText.DOColor(Color.white, 0.25f);
            });
        }
        else
        {

            // Fade to red
            increaseAmmoCapacityCostText.DOColor(Color.red, 0.25f).OnComplete(() =>
            {
                increaseAmmoCapacityCostText.DOColor(Color.white, 0.25f);
            });
        }
    }
}
