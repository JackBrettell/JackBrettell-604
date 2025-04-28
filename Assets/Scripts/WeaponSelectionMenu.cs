using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionMenu : MonoBehaviour
{
    public List<WeaponStats> weaponStatsList;
    public List<GunBehaviour> gunBehaviourList; // New field for matching guns
    public GameObject weaponButtonPrefab;
    public Transform contentPanel;

    public WeaponUpgrades weaponUpgradesPage;

    void Start()
    {
        PopulateWeaponButtons();
    }

    void PopulateWeaponButtons()
    {
        for (int i = 0; i < weaponStatsList.Count; i++)
        {
            GameObject buttonInstance = Instantiate(weaponButtonPrefab, contentPanel);
            WeaponButton buttonScript = buttonInstance.GetComponent<WeaponButton>();
            buttonScript.Setup(weaponStatsList[i], gunBehaviourList[i], weaponUpgradesPage);
        }
    }
}
