using TMPro;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

public class WeaponUpgradeView : MonoBehaviour
{
    public event Action OnDamageUpgradeClicked;
    public event Action OnFireRateUpgradeClicked;
    public event Action OnAmmoUpgradeClicked;

    [SerializeField] private TextMeshProUGUI damageUpgradeCostText;
    [SerializeField] private TextMeshProUGUI fireRateUpgradeCostText;
    [SerializeField] private TextMeshProUGUI ammoUpgradeCostText;

    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI currentMoneyText;

    public GameObject weaponButtonPrefab;
    public Transform contentPanel;


    public void Initialize(UpgradeCostsAndAmounts upgradeCostsAndAmounts)
    {
        //weaponNameText.text = .name;
        damageUpgradeCostText.text = upgradeCostsAndAmounts.damageUpgradeCost.ToString();
        fireRateUpgradeCostText.text = upgradeCostsAndAmounts.fireRateUpgradeCost.ToString();
        ammoUpgradeCostText.text = upgradeCostsAndAmounts.ammoUpgradeCost.ToString();
    }

    public void PopulateWeaponButtons(GunBehaviourBase[] gunBehaviour, WeaponStats[] weaponStats)
    {
        for (int i = 0; i < gunBehaviour.Length; i++)
        {
            GameObject buttonInstance = Instantiate(weaponButtonPrefab, contentPanel);
            WeaponButton buttonScript = buttonInstance.GetComponent<WeaponButton>();
            buttonScript.Setup(gunBehaviour[i], weaponStats[i]);
        }
    }


    public void UpdateMoney(int amount)
    {
        currentMoneyText.text = $"Money: £{amount}";
    }

    public void OnClick_UpgradeDamage() => OnDamageUpgradeClicked?.Invoke();
    public void OnClick_UpgradeFireRate() => OnFireRateUpgradeClicked?.Invoke();
    public void OnClick_UpgradeAmmo() => OnAmmoUpgradeClicked?.Invoke();

    public void AnimateDamageSuccess() => AnimateFeedback(damageUpgradeCostText, Color.green);
    public void AnimateFireRateSuccess() => AnimateFeedback(fireRateUpgradeCostText, Color.green);
    public void AnimateAmmoSuccess() => AnimateFeedback(ammoUpgradeCostText, Color.green);
 

    public void AnimateUpgradeFailure(WeaponUpgradeManager.UpgradeType type)
    {
        switch (type)
        {
            case WeaponUpgradeManager.UpgradeType.Damage:
                AnimateFeedback(damageUpgradeCostText, Color.red);
                Debug.Log("Damage upgrade failed");
                break;
            case WeaponUpgradeManager.UpgradeType.FireRate:
                AnimateFeedback(fireRateUpgradeCostText, Color.red);
                Debug.Log("Fire rate upgrade failed");
                break;
            case WeaponUpgradeManager.UpgradeType.Ammo:
                AnimateFeedback(ammoUpgradeCostText, Color.red);
                Debug.Log("Ammo upgrade failed");
                break;
        }
    }

    private void AnimateFeedback(TextMeshProUGUI text, Color color)
    {
        text.DOColor(color, 0.25f).OnComplete(() => text.DOColor(Color.white, 0.25f));
    }

}


