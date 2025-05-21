using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ShotgunBehaviour : GunBehaviour
{


 
    [SerializeField] private int pelletCount = 8;
    [SerializeField] private float spreadAngle = 5f;


 

    [SerializeField] private float triggerRecoilDuration = 0f;
    public override void Start()
    {
        base.Start();

        // Set gun part positions 
        gunOriginalPosition = gunTransform.localPosition;


    }
    public override void ReloadingSequence()
    {
        canFire = false;
        isRealoading = true;

        // Offsets and Durations
        Vector3 reloadOffset = Vector3.right * reloadMovement + Vector3.up * reloadMovementUp;
        Vector3 reloadRotation = new Vector3(0, 0, -25);
      


        // Stop any ongoing animations
        gunTransform.DOKill();
        gunMagTransform.DOKill();

        // Sequence
        Sequence reloadSequence = DOTween.Sequence();

        // Add magazine and weapon animations
        reloadSequence

            .Join(gunTransform.DOLocalMove(gunOriginalPosition + reloadOffset, reloadDuration).SetEase(EaseReload))
            // Sync rotation
            .Join(gunTransform.DOLocalRotate(reloadRotation, reloadDuration, RotateMode.Fast).SetEase(EaseReload))
            // Move slide back 
          //  .Join(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1))
            // Lower magazine
           // .Append(gunMagTransform.DOLocalMove(gunMagOffset, gunMagEjectDuration).SetEase(Ease.Linear))
            // Return magazine
            .Append(gunMagTransform.DOLocalMove(gunMagOriginalPosition, gunMagReturnDuration).SetEase(Ease.Linear))
            // Return weapon position
            .Append(gunTransform.DOLocalMove(gunOriginalPosition, reloadReturnDuration).SetEase(EaseReload))
            // Return weapon rotation
            .Join(gunTransform.DOLocalRotate(Vector3.zero, reloadReturnDuration, RotateMode.Fast).SetEase(EaseReload))
            // Delay 
            .AppendInterval(0.5f)
            // Move slide foward  
          //  .Append(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false))
            .OnComplete(() =>
            {
                ammoManager.Reload();
                canFire = true;




            });

    }







    private bool canFire = true;

    public override void Fire()
    {
        Vector3 recoilOffset = Vector3.back * recoilAmount;

        if (canFire && ammoManager.CurrentAmmo > 0)
        {
            canFire = false;

            for (int i = 0; i < pelletCount; i++)
            {
                Quaternion spreadRotation = Quaternion.Euler(
                    firePoint.rotation.eulerAngles.x + Random.Range(-spreadAngle, spreadAngle),
                    firePoint.rotation.eulerAngles.y + Random.Range(-spreadAngle, spreadAngle),
                    0
                );

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, spreadRotation);

                GunBullet bulletScript = bullet.GetComponent<GunBullet>();
                bulletScript.damage = weaponStats.damage;

                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.linearVelocity = bullet.transform.forward * bulletSpeed;
            }


            Sequence firingSequence = DOTween.Sequence();
            firingSequence
                .Append(trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast))
               // .Join(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1))
                .Join(gunTransform.DOLocalMove(gunOriginalPosition + recoilOffset, recoilDuration, false).SetEase(Ease1))
                .Append(trigger.DOLocalRotate(gunTriggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast))
              //  .Join(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false))
                .Join(gunTransform.DOLocalMove(gunOriginalPosition, recoilRecoverySpeed, false));

            ammoManager.ReduceAmmo();

            DOVirtual.DelayedCall(1f / weaponStats.fireRate, () => { canFire = true; });
        }
        else if (canFire && ammoManager.CurrentAmmo == 0)
        {
            Sequence firingEmptySequence = DOTween.Sequence();
           // firingEmptySequence
               // .Append(trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast))
              //  .Join(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1))
                //.Append(trigger.DOLocalRotate(gunTriggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast))
            //    .Join(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false));
        }
    }

    /*else if (currentAmmoCount == 0)
    {
        // Handle empty fire animations
        Sequence emptyFireSequence = DOTween.Sequence();


        emptyFireSequence.Append(trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast))                // Trigger down
                         .Append(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1)) // Slide 
                         .Join(trigger.DOLocalRotate(pistolTriggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast));           // Trigger return 


    }*/
}



/* private IEnumerator CrossHairScale()
 {
     float originalSize = crosshairSize;
     float targetSize = originalSize * 1.5f;
     float duration = 1f;
     float elapsedTime = 0f;

     // Scale up the crosshair
     while (elapsedTime < duration / 2)
     {
         elapsedTime += Time.deltaTime;
         float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / (duration / 2));
         crosshair.transform.localScale = new Vector3(newSize, newSize, newSize);
         yield return null;
     }

     // Scale back 
     elapsedTime = 0f;
     while (elapsedTime < duration / 2)
     {
         elapsedTime += Time.deltaTime;
         float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / (duration / 2));
         crosshair.transform.localScale = new Vector3(newSize, newSize, newSize);
         yield return null;
     }

     // Ensure final size is original
     crosshair.transform.localScale = new Vector3(originalSize, originalSize, originalSize);
 }

 private IEnumerator Recoil()
 {
     Vector3 originalPosition = pistolTransform.localPosition;
     Vector3 recoilOffset = Vector3.back * recoilAmount;

     float elapsedTime = 0f;

     // Apply recoil
     while (elapsedTime < 0.1f)
     {
         elapsedTime += Time.deltaTime;
         pistolTransform.localPosition = Vector3.Lerp(originalPosition, originalPosition + recoilOffset, elapsedTime / 0.1f);
         yield return null;
     }

     // Recoil recovery
     elapsedTime = 0f;
     while (elapsedTime < 1f / recoilRecoverySpeed)
     {
         elapsedTime += Time.deltaTime;
         pistolTransform.localPosition = Vector3.Lerp(pistolTransform.localPosition, originalPosition, elapsedTime * recoilRecoverySpeed);
         yield return null;
     }

     // Ensure final position is original
     pistolTransform.localPosition = originalPosition;
 }

 */


