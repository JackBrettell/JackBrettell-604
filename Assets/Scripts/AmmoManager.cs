using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] private int maxAmmo; 
    private int currentAmmo;

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;

    public void Initialize(int maxAmmo)
    {
        this.maxAmmo = maxAmmo;
        currentAmmo = maxAmmo;
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    public void ReduceAmmo()
    {
        if (currentAmmo > 0)
            currentAmmo--;
    }
}
