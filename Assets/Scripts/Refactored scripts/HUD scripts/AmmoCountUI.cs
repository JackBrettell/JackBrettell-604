using UnityEngine;
using UnityEngine.UI;

public class AmmoCountUI : MonoBehaviour
{
    [SerializeField] private Image ammoValueFill;
    [SerializeField] private Image ammoeIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UpdateAmmoBar(int currentAmmo, int maxAmmo)
    {
        float progress = (float)currentAmmo / maxAmmo;
        ammoValueFill.fillAmount = progress;
        Debug.Log("Current ammo: " + currentAmmo + "Max ammo: " + maxAmmo);
    }
}
