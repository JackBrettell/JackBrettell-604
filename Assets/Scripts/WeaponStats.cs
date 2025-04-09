using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapon/Stats")]
public class WeaponStats : ScriptableObject
{

        public int damage;
        public float fireRate;
        public int ammoCapacity;

        // Menu
        [Header("Menu")]
        public string weaponName;
        public string weaponDescription;
        public Sprite weaponIcon;




}
