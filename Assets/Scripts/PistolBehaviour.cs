using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PistolBehaviour : GunBehaviour
{


    [Header("Slide")]
    [SerializeField] private float slideRecoveryDuration = 0f;
    [SerializeField] private float slideRecoilDuration = 0f;
    [SerializeField] private float slideRecoil = 0f;
    private Vector3 slideOriginalPosition;
    public Transform slide;


    [Header("Reload")]
    private Transform pistolMagTransform;
    private Vector3 pistolMagOriginalPosition;

    public float pistolMagMovemement = 0;
    public float pistolMagEjectDuration = 0;
    public float pistolMagReturnDuration = 0;

    [SerializeField] private float triggerRecoilDuration = 0f;
    public override void Start()
    {
        base.Start();

        // Set default damage

        damage = weaponStats.damage;

        // Set gun part positions 
        gunOriginalPosition = gunTransform.localPosition;
        slideOriginalPosition = slide.localPosition;
        gunTriggerOriginalPosition = trigger.localPosition;
        gunMagOriginalPosition = gunMagTransform.localPosition;


    }
    public override void ReloadingSequence()
    {
        if (isRealoading) return;

        canFire = false;
        isRealoading = true;
        isSwayEnabled = false;
        isCamRayLookAtEnabled = false;

        Vector3 reloadOffset = Vector3.right * reloadMovement + Vector3.up * reloadMovementUp;
        Vector3 reloadRotation = new Vector3(0, 0, -25);
        Vector3 gunMagOffset = gunMagOriginalPosition + Vector3.down * gunMagMovemement;
        Vector3 slideOffset = Vector3.back * slideRecoil;

        gunTransform.DOKill();
        gunMagTransform.DOKill();
        slide.DOKill();

        Sequence reloadSequence = DOTween.Sequence();

        // Step 1: Slide recoil
        reloadSequence.Append(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration).SetEase(Ease1));

        // Step 2: Gun moves to reload pose
        reloadSequence.Append(gunTransform.DOLocalMove(gunOriginalPosition + reloadOffset, reloadDuration).SetEase(EaseReload));
        reloadSequence.Join(gunTransform.DOLocalRotate(reloadRotation, reloadDuration, RotateMode.Fast).SetEase(EaseReload));

        // Step 3: Pause in reload pose

        // Step 4: Magazine out/in
        reloadSequence.Append(gunMagTransform.DOLocalMove(gunMagOffset, gunMagEjectDuration).SetEase(Ease.Linear));
        reloadSequence.Append(gunMagTransform.DOLocalMove(gunMagOriginalPosition, gunMagReturnDuration).SetEase(Ease.Linear));
        reloadSequence.AppendInterval(0.5f);

        //  Step 5: Gun returns to original position and rotation
        reloadSequence.Append(gunTransform.DOLocalMove(gunOriginalPosition, reloadReturnDuration).SetEase(EaseReload));
        reloadSequence.Join(gunTransform.DOLocalRotate(Vector3.zero, reloadReturnDuration, RotateMode.Fast).SetEase(EaseReload));

        // Step 6: Slide returns to forward position
        reloadSequence.Append(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration));

        // Complete
        reloadSequence.OnComplete(() =>
        {
            ammoManager.Reload();
            



            canFire = true;
            isRealoading = false;
            isCamRayLookAtEnabled = false;
            isSwayEnabled = true;
        });

    }










    private bool canFire = true;

    public override void Fire()
    {
        base.Fire();

        // Offsets and Durations
        Vector3 recoilOffset = Vector3.back * recoilAmount;
        Vector3 slideOffset = Vector3.back * slideRecoil;

        if (canFire && ammoManager.CurrentAmmo > 0)
        {
            canFire = false;

            // Play sound
            AudioClip fireSound = weaponStats.fireSound;
            audioSource.PlayOneShot(fireSound);

            // Retrieve the bullet from the pool
            GunBullet bullet = BulletPool.Instance.GetBullet();

            // Set the bullet's position and rotation before firing
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            // Set the bullet's damage
            GunBullet bulletScript = bullet.GetComponent<GunBullet>();
            bulletScript.damage = damage;


            // Add velocity to the bullet
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.linearVelocity = firePoint.forward * bulletSpeed;

            Sequence firingSequence = DOTween.Sequence();
            firingSequence
                          // Trigger down
                          .Append(trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast))
                          // Slide goes back
                          .Join(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1))
                          // Recoil send weapon back
                          .Join(gunTransform.DOLocalMove(gunOriginalPosition + recoilOffset, recoilDuration, false).SetEase(Ease1))
                          // Trigger returns
                          .Append(trigger.DOLocalRotate(gunTriggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast))
                          // Slide returns
                          .Join(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false))
                          // Recoil returns
                          .Join(gunTransform.DOLocalMove(gunOriginalPosition, recoilRecoverySpeed, false));


            ammoManager.ReduceAmmo();

            DOVirtual.DelayedCall(1f / fireRate, () => { canFire = true; });
        }
        else if (canFire && ammoManager.CurrentAmmo == 0)
        {
            Sequence firingEmptySequence = DOTween.Sequence();
            firingEmptySequence

                          // Trigger down
                          .Append(trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast))
                          // Slide goes back
                          .Join(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1))
                          // Trigger returns
                          .Append(trigger.DOLocalRotate(gunTriggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast))
                          // Slide returns
                          .Join(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false));

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
}