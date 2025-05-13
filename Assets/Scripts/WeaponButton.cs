using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponButton : MonoBehaviour
{
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponDescText;
    public TextMeshProUGUI weaponCostText;
    public Image weaponIconImage;

    private WeaponStats weaponStats;
    private GunBehaviour gunBehaviour;
    private StoreMenus storeMenus;

    private Image buttonImage;


    public void Setup(GunBehaviour gun, WeaponStats weapon)
    {
        weaponStats = Instantiate(weapon); // Clone the weapon for independent upgrades
        gunBehaviour = gun;
        storeMenus = FindObjectOfType<StoreMenus>();
        buttonImage = GetComponent<Image>();

        weaponNameText.text = weapon.weaponName;
        weaponDescText.text = weapon.weaponDescription;
        weaponIconImage.sprite = weapon.weaponIcon;
        weaponCostText.text = weapon.isUnlocked ? "Unlocked" : $"Cost: " + "£" + weapon.cost;

        GetComponent<Button>().onClick.AddListener(OpenUpgradePage);
    }

    public void OpenUpgradePage()
    {
        // Check if the weapon is unlocked before opening the upgrade page
        if (gunBehaviour.isWeaponUnlocked)
        {
            // Get the current money to update the UI
            int currentMoney = MoneyManager.Instance.CurrentMoney;

            storeMenus.OnWeaponsUpgrades();
        }
        else // if the weapon is not unlocked the player will buy it 
        {
            // Check if the player has enough money to unlock the weapon
            if (MoneyManager.Instance.CurrentMoney >= weaponStats.cost)
            {

                // Fade to green if can afford
                buttonImage.DOColor(Color.green, 0.25f).OnComplete(() =>
                {
                    buttonImage.DOColor(Color.white, 0.25f);

                    // Deduct the cost, and Unlock
                    MoneyManager.Instance.RemoveMoney(weaponStats.cost);
                    gunBehaviour.isWeaponUnlocked = true;
                    weaponCostText.text = "Unlocked";

                });
            }
            else
            {
                // Fade to red if cant afford
                buttonImage.DOColor(Color.red, 0.25f).OnComplete(() =>
                {
                    buttonImage.DOColor(Color.white, 0.25f);
                });
            }
        }





    }

}
