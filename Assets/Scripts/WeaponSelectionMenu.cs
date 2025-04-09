using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionMenu : MonoBehaviour
{
    public List<WeaponStats> weaponStatsList;
    public GameObject weaponButtonPrefab;
    public Transform contentPanel;

    public WeaponUpgrades weaponUpgradesPage;

    void Start()
    {
        PopulateWeaponButtons();
    }

    void PopulateWeaponButtons()
    {
        foreach (var weapon in weaponStatsList)
        {
            GameObject buttonInstance = Instantiate(weaponButtonPrefab, contentPanel);
            WeaponButton buttonScript = buttonInstance.GetComponent<WeaponButton>();
            buttonScript.Setup(weapon, weaponUpgradesPage);
        }
    }
}
