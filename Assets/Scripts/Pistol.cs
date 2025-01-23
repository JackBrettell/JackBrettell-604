using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using DG;
using DG.Tweening;
using System.Collections.Generic;
using System;
using TMPro;

public class Pistol : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int ammoCapacity;
    [SerializeField] private float fireRate;
    public TextMeshProUGUI ammoDisplay;
    private Transform firePoint;
    private int currentAmmoCount;

    [Header("Crosshair")]
    private GameObject crosshair;
    private float crosshairSize = 0.5f;

    [Header("Recoil")]
    [SerializeField] private float recoilAmount = 0.1f;
    [SerializeField] private float recoilRecoverySpeed = 5f;
    [SerializeField] private float recoilDuration = 0;
    private Vector3 pistolOriginalPosition;
    private Transform pistolTransform;
    private Ease Ease1 = Ease.Linear;
    private Ease Ease2 = Ease.Linear;

    [Header("Slide")]
    [SerializeField] private float slideRecoveryDuration = 0f;
    [SerializeField] private float slideRecoilDuration = 0f;
    [SerializeField] private float slideRecoil = 0f;
    private Vector3 slideOriginalPosition;
    private Transform slide;

    [Header("Trigger")]
    [SerializeField] private float triggerRecoilDuration = 0f;
    [SerializeField] private float triggerRecoveryDuration = 0f;
    private Vector3 pistolTriggerOriginalPosition;
    private Transform trigger;
    private Vector3 triggerDownRotation = new Vector3(45, 0, 0);

    [Header("Reload")]
    public Ease EaseReload = Ease.InBounce;
    public float reloadMovement = 0.01f;
    public float reloadMovementUp = 0.01f;
    public float reloadDuration = 1;
    public float reloadReturnDuration = 1;

    private Transform pistolMagTransform;
    private Vector3 pistolMagOriginalPosition;
    private bool isRealoading = false;
    
    public float pistolMagMovemement = 0;
    public float pistolMagEjectDuration = 0;
    public float pistolMagReturnDuration = 0;
   


    public void Start()
    {

        // ===== Auto-assigns =====
        // HUD
        crosshair = GameObject.Find("Crosshair");
        //ammoCapacity = GameObject.Find("Ammo");
        
        // Fire point
        GameObject firePointObject = GameObject.Find("pistol_muzzle");
        firePoint = firePointObject.transform;

        // Weapon
        GameObject gunTransform = GameObject.Find("Pistol");
        pistolTransform = gunTransform.transform;

        // Slide
        GameObject slideTransform = GameObject.Find("pistol_slide");
        slide = slideTransform.transform;

        // Trigger
        GameObject triggerTransform = GameObject.Find("pistol_trigger");
        trigger = triggerTransform.transform;

        // Magazine
        GameObject magTransform = GameObject.Find("pistol_mag");
        pistolMagTransform = magTransform.transform;

        


        // Set gun part positions 
        pistolOriginalPosition = pistolTransform.localPosition;
        slideOriginalPosition = slide.localPosition;
        pistolTriggerOriginalPosition = trigger.localPosition;
        pistolMagOriginalPosition = pistolMagTransform.localPosition;

        currentAmmoCount = ammoCapacity;
        ammoDisplay.text = currentAmmoCount.ToString();

    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed && !isRealoading)
        {
            isRealoading = true;

            // Offsets and Durations
            Vector3 reloadOffset = Vector3.right * reloadMovement + Vector3.up * reloadMovementUp;
            Vector3 reloadRotation = new Vector3(0, 0, -25);
            Vector3 pistolMagOffset = pistolMagOriginalPosition + Vector3.down * pistolMagMovemement;
            Vector3 slideOffset = Vector3.back * slideRecoil;


            // Stop any ongoing animations
            pistolTransform.DOKill();
            pistolMagTransform.DOKill();

            // Sequence
            Sequence reloadSequence = DOTween.Sequence();

            // Add magazine and weapon animations
            reloadSequence
                
                .Join(pistolTransform.DOLocalMove(pistolOriginalPosition + reloadOffset, reloadDuration).SetEase(EaseReload))
                // Sync rotation
                .Join(pistolTransform.DOLocalRotate(reloadRotation, reloadDuration, RotateMode.Fast).SetEase(EaseReload))
                // Move slide back 
                .Join(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1))
                // Lower magazine
                .Append(pistolMagTransform.DOLocalMove(pistolMagOffset, pistolMagEjectDuration).SetEase(Ease.Linear))
                // Return magazine
                .Append(pistolMagTransform.DOLocalMove(pistolMagOriginalPosition, pistolMagReturnDuration).SetEase(Ease.Linear))
                // Return weapon position
                .Append(pistolTransform.DOLocalMove(pistolOriginalPosition, reloadReturnDuration).SetEase(EaseReload))
                // Return weapon rotation
                .Join(pistolTransform.DOLocalRotate(Vector3.zero, reloadReturnDuration, RotateMode.Fast).SetEase(EaseReload))
                // Delay 
                .AppendInterval(0.5f)
                // Move slide foward  
                .Append(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false))                                               
                .OnComplete(() =>
                {
                    currentAmmoCount = ammoCapacity;
                    ammoDisplay.text = currentAmmoCount.ToString();

                    isRealoading = false;

                });

        }
    }







    private bool canFire = true; // Controls firing cooldown

    public void OnFire(InputAction.CallbackContext context)
    {
        // Offsets and Durations
        Vector3 recoilOffset = Vector3.back * recoilAmount;
        Vector3 slideOffset = Vector3.back * slideRecoil;

        if (context.performed && currentAmmoCount != 0 && !isRealoading && canFire)
        {
            currentAmmoCount--;
            canFire = false; // Disable firing temporarily to respect fire rate

            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Add velocity to the bullet
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.linearVelocity = firePoint.forward * bulletSpeed;



            // Create firing sequence
            Sequence firingSequence = DOTween.Sequence();

            // Crosshair scaling
            firingSequence
                          // Trigger down
                          .Append(trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast))
                          // Crosshair scale up
                          .Append(crosshair.transform.DOScale(1.5f, 0.1f))
                          // Slide goes back
                          .Join(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1))   
                          // Recoil send weapon back
                          .Join(pistolTransform.DOLocalMove(pistolOriginalPosition + recoilOffset, recoilDuration, false).SetEase(Ease1))
                          // Trigger returns
                          .Append(trigger.DOLocalRotate(pistolTriggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast))         
                          // Slide returns
                          .Join(slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false))
                          // Recoil returns
                          .Join(pistolTransform.DOLocalMove(pistolOriginalPosition, recoilRecoverySpeed, false))
                          // Crosshair scale down
                          .Join(crosshair.transform.DOScale(1f, 0.5f));   






            // Update ammo display
            ammoDisplay.text = currentAmmoCount.ToString();

            // Restore firing ability after cooldown
            DOVirtual.DelayedCall(1f / fireRate, () => { canFire = true; });
        }
        else if (context.performed && currentAmmoCount == 0)
        {
            // Handle empty fire animations
            Sequence emptyFireSequence = DOTween.Sequence();


            emptyFireSequence.Append(trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast))                // Trigger down
                             .Append(slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1)) // Slide 
                             .Join(trigger.DOLocalRotate(pistolTriggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast));           // Trigger return 

        
        }
    }



    private IEnumerator CrossHairScale()
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


}