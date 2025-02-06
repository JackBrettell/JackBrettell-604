using TMPro;
using System;
using UnityEngine;
public class AmmoManager : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 30;  // Default value, can be set in Inspector
    private int currentAmmo;

    public int CurrentAmmo => currentAmmo; // Read current ammo
    public int MaxAmmo => maxAmmo; // Read max ammo

    private void Awake()
    {
        currentAmmo = maxAmmo; // Initialize ammo properly
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    public void ReduceAmmo()
    {
        currentAmmo--;
    }
}


