using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using DG;
using DG.Tweening;
using System.Collections.Generic;

public class WeaponBase : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    [Header("Crosshair")]
    public GameObject crosshair;
    private float crosshairSize = 0.5f;

    [Header("Recoil")]
    public Transform weaponTransform;
    public float recoilAmount = 0.1f;
    public float recoilRecoverySpeed = 5f;
    public float recoilDuration = 0;
    private Vector3 weaponOriginalPosition;

    public Ease Ease1 = Ease.InElastic ;
    public Ease Ease2 = Ease.InElastic ;

    [Header("Slide")]
    public Vector3 slideOriginalPosition;
    public Transform slide;
    public float slideRecoveryDuration = 0f;
    public float slideRecoilDuration = 0f;
    public float slideRecoil = 0f;

    public Vector3 triggerOriginalPosition;
    public Transform trigger;
    public float triggerRecoilDuration = 0f;
    public float triggerRecoveryDuration = 0f;
    private Vector3 triggerDownRotation = new Vector3(45,0,0);


    public void Start()
    {
        weaponOriginalPosition = weaponTransform.localPosition;
        slideOriginalPosition = slide.localPosition;
        triggerOriginalPosition = trigger.localPosition;
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Instantiate 
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Add velocity
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.linearVelocity = firePoint.forward * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("WeaponBase: The bullet prefab is missing a Rigidbody component.");
            }

            // Scale crosshair
           //StartCoroutine(CrossHairScale());

            // Apply recoil
          // StartCoroutine(Recoil());


            // Scale crosshair up and down
            crosshair.transform.DOKill();
            crosshair.transform.DOScale(1.5f, 0.1f) // Scale up to 1.5x in 0.1 seconds
                     .OnComplete(() => crosshair.transform.DOScale(1f, 0.5f)); // Scale back to 1x in 0.1 seconds

            //Recoil
            Vector3 recoilOffset = Vector3.back * recoilAmount;

            weaponTransform.DOKill();
            weaponTransform.DOLocalMove(weaponOriginalPosition + recoilOffset, recoilDuration, false).SetEase(Ease1)
                .OnComplete(() => weaponTransform.DOLocalMove(weaponOriginalPosition, recoilRecoverySpeed, false));

           

            //.SetLoops(2, LoopType.Yoyo);
/*
            //Trigger

            trigger.DOKill();
            trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast)
                .OnComplete(() => trigger.transform.DOLocalRotate(triggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast));        
            
            //Trigger
*/
            trigger.DOKill();
            trigger.DOLocalRotate(triggerDownRotation, triggerRecoilDuration, RotateMode.Fast).OnComplete(() =>
            {

                     trigger.transform.DOLocalRotate(triggerOriginalPosition, triggerRecoveryDuration, RotateMode.Fast);
                //Slide
                 Vector3 slideOffset = Vector3.back * slideRecoil;

                slide.DOKill();
                slide.DOLocalMove(slideOriginalPosition + slideOffset, slideRecoilDuration, false).SetEase(Ease1)
                    .OnComplete(() => slide.DOLocalMove(slideOriginalPosition, slideRecoveryDuration, false));
            });



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
        Vector3 originalPosition = weaponTransform.localPosition;
        Vector3 recoilOffset = Vector3.back * recoilAmount;

        float elapsedTime = 0f;

        // Apply recoil
        while (elapsedTime < 0.1f)
        {
            elapsedTime += Time.deltaTime;
            weaponTransform.localPosition = Vector3.Lerp(originalPosition, originalPosition + recoilOffset, elapsedTime / 0.1f);
            yield return null;
        }

        // Recoil recovery
        elapsedTime = 0f;
        while (elapsedTime < 1f / recoilRecoverySpeed)
        {
            elapsedTime += Time.deltaTime;
            weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, originalPosition, elapsedTime * recoilRecoverySpeed);
            yield return null;
        }

        // Ensure final position is original
        weaponTransform.localPosition = originalPosition;
    }


}