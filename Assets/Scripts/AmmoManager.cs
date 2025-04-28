using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private int ammoCapacity;
    private int currentAmmo;

    public void Initialize(GunBehaviour gun)
    {
        ammoCapacity = gun.AmmoCapacity;
        currentAmmo = ammoCapacity;
    }

    public int CurrentAmmo => currentAmmo;

    public void ReduceAmmo()
    {
        if (currentAmmo > 0)
            currentAmmo--;
    }

    public void Reload()
    {

        currentAmmo = ammoCapacity;
    }
    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo = Mathf.Clamp(ammo, 0, ammoCapacity);
    }

}