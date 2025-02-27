using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapon/Stats")]
public class WeaponStats : ScriptableObject
{
    public int damage;
    public float fireRate;
    public int ammoCapacity;

}
