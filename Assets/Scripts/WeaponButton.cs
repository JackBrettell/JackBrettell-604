using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponDescText;
    public Image weaponIconImage;

    private WeaponStats weaponStats;
    private GunBehaviour gunBehaviour;
    private WeaponUpgrades upgradePage;
    private StoreMenus storeMenus;

    public void Setup(WeaponStats weapon, GunBehaviour gun, WeaponUpgrades upgradeScript)
    {
        weaponStats = Instantiate(weapon); // Clone the weapon for independent upgrades
        gunBehaviour = gun;
        upgradePage = upgradeScript;
        storeMenus = FindObjectOfType<StoreMenus>();

        weaponNameText.text = weapon.weaponName;
        weaponDescText.text = weapon.weaponDescription;
        weaponIconImage.sprite = weapon.weaponIcon;

        GetComponent<Button>().onClick.AddListener(OpenUpgradePage);
    }

    public void OpenUpgradePage()
    {
        storeMenus.OnWeaponsUpgrades();
        upgradePage.Initialize(weaponStats, gunBehaviour);
    }
}
