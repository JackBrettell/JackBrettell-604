using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponDescText;
    public TextMeshProUGUI weaponCostText;
    public Image weaponIconImage;

    private WeaponStats weaponStats;
    private GunBehaviour gunBehaviour;
    private WeaponUpgrades weaponUpgrades;
    private StoreMenus storeMenus;


    public void Setup(WeaponStats weapon, GunBehaviour gun, WeaponUpgrades upgradeScript)
    {
        weaponStats = Instantiate(weapon); // Clone the weapon for independent upgrades
        gunBehaviour = gun;
        weaponUpgrades = upgradeScript;
        storeMenus = FindObjectOfType<StoreMenus>();

        weaponNameText.text = weapon.weaponName;
        weaponDescText.text = weapon.weaponDescription;
        weaponIconImage.sprite = weapon.weaponIcon;
        weaponCostText.text = weapon.isUnlocked ? "Unlocked" : $"Cost: " + "£" + weapon.cost;

        GetComponent<Button>().onClick.AddListener(OpenUpgradePage);
    }

    public void OpenUpgradePage()
    {
        // Get the current money to update the UI
        int currentMoney = MoneyManager.Instance.CurrentMoney;

        weaponUpgrades.currentMoneyText.text = $"Money: £{currentMoney}";
        storeMenus.OnWeaponsUpgrades();
        weaponUpgrades.Initialize(weaponStats, gunBehaviour);


    }

}
