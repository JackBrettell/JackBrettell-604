using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponSelectionMenu : MonoBehaviour
{
    public List<WeaponStats> weaponStatsList;  // Assign from Inspector
    public GameObject weaponButtonPrefab;      // Assign the button prefab
    public Transform contentPanel;             // Assign the ScrollView Content

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
            buttonScript.Setup(weapon);
        }
    }
}
