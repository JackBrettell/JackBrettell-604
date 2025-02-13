using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Collections;

public class RifleBehaviour : GunBehaviour
{
    
    public override void Start()
    {
        // ===== Auto-assigns =====

        // Weapon base scrip
       // weaponBase = GetComponent<WeaponBase>();
        base.Start(); // Call the base Start()

        // Initialize AmmoManager with ammo capacity





        /*
                // Fire point
                GameObject firePointObject = GameObject.Find("gun_muzzle");
                firePoint = firePointObject.transform;

                // Weapon
                GameObject gunTransform = GameObject.Find("gun");
                gunTransform = gunTransform.transform;

                // Trigger
                GameObject triggerTransform = GameObject.Find("gun_trigger");
                trigger = triggerTransform.transform;

                // Magazine
                GameObject magTransform = GameObject.Find("gun_mag");
                gunMagTransform = magTransform.transform;*/



        // Set gun part positions 
        gunOriginalPosition = gunTransform.localPosition;
        gunTriggerOriginalPosition = trigger.localPosition;
        gunMagOriginalPosition = gunMagTransform.localPosition;




    }

    public override void FiringSequence()
    {
        Sequence firingSequence = DOTween.Sequence();
        Vector3 recoilOffset = Vector3.back * recoilAmount + Vector3.up * recoilUpAmount;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's damage
        GunBullet bulletScript = bullet.GetComponent<GunBullet>();
        bulletScript.damage = damage;
       

        // Add velocity to the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = firePoint.forward * bulletSpeed;

        firingSequence
            .Join(gunTransform.DOLocalMove(gunOriginalPosition + recoilOffset, recoilDuration, false).SetEase(Ease1))
            .Append(gunTransform.DOLocalMove(gunOriginalPosition, recoilRecoverySpeed, false));

    }


    public override void ReloadingSequence()
    {
        base.ReloadingSequence(); // Call base reload logic

        Vector3 reloadOffset = Vector3.right * reloadMovement + Vector3.up * reloadMovementUp;
        Vector3 reloadRotation = new Vector3(0, 0, -25);
        Vector3 reloadRotationUp = new Vector3(-40, 0, 0);

        Vector3 gunMagOffsetDowm = gunMagOriginalPosition + Vector3.down * gunMagMovemement;
        Vector3 gunMagOffsetBack = gunMagOriginalPosition + Vector3.back * gunMagMovemement;



        Sequence reloadingSequence = DOTween.Sequence();

        reloadingSequence
                .Join(gunTransform.DOLocalMove(gunOriginalPosition + reloadOffset, reloadDuration).SetEase(EaseReload))
                .Join(gunTransform.DOLocalRotate(reloadRotation, reloadDuration, RotateMode.Fast).SetEase(EaseReload))
                .Join(gunTransform.DOLocalRotate(reloadRotationUp, reloadDuration, RotateMode.Fast).SetEase(EaseReload))

                // Lower magazine
                .Append(gunMagTransform.DOLocalMove(gunMagOffsetDowm, gunMagEjectDuration).SetEase(Ease.Linear))
                .Append(gunMagTransform.DOLocalMove(gunMagOffsetBack, gunMagEjectDuration).SetEase(Ease.Linear))

                // Return magazine
                .Append(gunMagTransform.DOLocalMove(gunMagOffsetDowm, gunMagReturnDuration).SetEase(Ease.Linear))
                .Append(gunMagTransform.DOLocalMove(gunMagOriginalPosition, gunMagReturnDuration).SetEase(Ease.Linear))

                .Append(gunTransform.DOLocalMove(gunOriginalPosition, reloadReturnDuration).SetEase(EaseReload))
                .Join(gunTransform.DOLocalRotate(Vector3.zero, reloadReturnDuration, RotateMode.Fast).SetEase(EaseReload))
                .OnComplete(() =>
                {
                    // finished reloading
                    ammoManager.Reload();
                    hud.updateAmmoCount();
                });
    }

    public override void Fire()
    {
        base.Fire();

        if (ammoManager.CurrentAmmo > 0)
        {
            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine(AutoFireRifle());
            }
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }
    public override void StopFire()
    {
        base.StopFire(); 

        isFiring = false;
    }


    private IEnumerator AutoFireRifle()
    {
        while (isFiring && ammoManager.CurrentAmmo > 0)
        {
            FireRifle();
            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    public void FireRifle()
    {
        if (ammoManager.CurrentAmmo > 0)
        {
            ammoManager.ReduceAmmo();
            hud.updateAmmoCount();
            FiringSequence();
        }
        else
        {
            isFiring = false;
        }
    }
}
