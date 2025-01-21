using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
            StartCoroutine(CrossHairScale());

            // Apply recoil
            StartCoroutine(Recoil());
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