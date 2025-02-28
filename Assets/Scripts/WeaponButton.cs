using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponButton : MonoBehaviour
{
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponDescText;
    public Sprite weaponIconImage;
    private WeaponStats weaponStats;

    public void Setup(WeaponStats weapon)
    {
        weaponStats = weapon;
        weaponNameText.text = weapon.weaponName;
        weaponDescText.text = weapon.weaponDescription;
        weaponIconImage = weapon.weaponIcon;
    }
}
