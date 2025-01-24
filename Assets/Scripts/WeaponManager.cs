using UnityEngine;
using DG;
using UnityEngine.InputSystem;
using DG.Tweening;

public class WeaponManager : MonoBehaviour
{
    private RifleBehaviour rifleBehaviour;  



    public GameObject[] weapons;

    [HideInInspector] public bool isPistolEquipped = false;
    [HideInInspector] public bool isRifleEquipped = false;

    public void Start()
    {
        rifleBehaviour = GetComponent<RifleBehaviour>();

    }

    public void OnWeaponSwitch(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            var control = context.control;

            if (int.TryParse(control.name, out int weaponChoice))
            {
                SwitchToWeapon(weaponChoice); 
            }
        }
    }

    private void SwitchToWeapon(int weaponIndex)
    {
        if (weaponIndex == 1)
        {
            isPistolEquipped = true;
            isRifleEquipped = false;
            // Sequence
            Sequence pistolEquipSequence = DOTween.Sequence();

         
           // pistolEquipSequence
                // Sync with gun movement
              //  .Join(weaponTransform.DOLocalMove(weaponOriginalPosition + reloadOffset, reloadDuration).SetEase(EaseReload))

            weapons[1].SetActive(false);
            weapons[0].SetActive(true);
        }
        else if (weaponIndex == 2) 
        {

            isRifleEquipped = true;
            isPistolEquipped = false;

            weapons[1].SetActive(true);
            weapons[0].SetActive(false);

        }
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
