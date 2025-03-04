using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponSelectionMenu : MonoBehaviour
{
    public List<WeaponStats> weaponStatsList;  
    public GameObject weaponButtonPrefab;      
    public Transform contentPanel;             
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

           Debug.Log(weapon.weaponIcon.name);

        }
    }
}
