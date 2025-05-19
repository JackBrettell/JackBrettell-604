using DG.Tweening;
using UnityEngine;
using System;

public class GunBehaviour : MonoBehaviour
{
    public WeaponInputManager weaponBase;
    public AmmoManager ammoManager;
    public HUD hud;


   public bool isWeaponUnlocked = false;

    public Action OnWeaponFired;

    [Header("Weapon Stats")]
    public WeaponStats weaponStats;

    [SerializeField] private Camera playerCamera;


    [Header("Weapon Settings")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float bulletSpeed = 20f;
    [SerializeField] protected int damage;
    [HideInInspector] public int Damage => damage;

    [SerializeField] protected int ammoCapacity = 30;
    public int AmmoCapacity => ammoCapacity;

    [SerializeField] protected float fireRate;
    [SerializeField] protected Transform firePoint;

    [Header("Recoil")]
    [SerializeField] protected float recoilAmount = 0.1f;
    [SerializeField] protected float recoilUpAmount = 0.1f;
    [SerializeField] protected float recoilRecoverySpeed = 5f;
    [SerializeField] protected float recoilDuration = 0;
    protected Vector3 gunOriginalPosition;
    [SerializeField] protected Transform gunTransform;
    protected Ease Ease1 = Ease.Linear;
    protected Ease Ease2 = Ease.Linear;

    [Header("Trigger")]
    [SerializeField] protected float triggerDownDuration = 0f;
    [SerializeField] protected float triggerRecoveryDuration = 0f;
    protected Vector3 gunTriggerOriginalPosition;
    [SerializeField] protected Transform trigger;
    protected Vector3 triggerDownRotation = new Vector3(45, 0, 0);

    [Header("Reload")]
    public Ease EaseReload = Ease.InBounce;
    public float reloadMovement = 0.01f;
    public float reloadMovementUp = 0.01f;
    public float reloadDuration = 1;
    public float reloadReturnDuration = 1;

    [SerializeField] protected Transform gunMagTransform;
    protected Vector3 gunMagOriginalPosition;
    protected Quaternion gunInitialRotation;
    protected bool isRealoading = false;

    public float gunMagMovemement = 0;
    public float gunMagEjectDuration = 0;
    public float gunMagReturnDuration = 0;

    protected bool isReloading = false;
    protected bool isFiring = false;

    [Header("Sway Settings")]
    protected bool isSwayEnabled = true; // Enable or disable weapon sway
    protected bool isCamRayLookAtEnabled = true; // Enable or disable gun targeting camera ray
    [SerializeField] private float swayAmount = 0.02f;      // How much the gun moves
    [SerializeField] private float maxSwayAmount = 0.06f;   // Max movement limit
    [SerializeField] private float swaySmoothness = 5f;     // Damping effect
    [SerializeField] private float rotationSwayAmount = 4f; // Rotation amount

    [Header("Sound")]
    [SerializeField] protected AudioSource audioSource;



    public virtual void Start()
    {
        // ===== Auto-assigns =====

        // Weapon base scrip
        //  weaponBase = GetComponent<WeaponBase>();
        //  ammoManager = GetComponent<AmmoManager>();

        if (ammoManager == null)
        {
            ammoManager = GetComponent<AmmoManager>();
        }

        // Pass the ammo stats to AmmoManager
        if (ammoManager == null)
        {
            ammoManager = GetComponent<AmmoManager>();
        }


        if (weaponStats != null)
        {
            // Pass the ammo capacity from WeaponStats to AmmoManager
            ammoCapacity = weaponStats.ammoCapacity;
            ammoManager.Initialize(this); // Pass GunBehaviour

            // Set isWeaponUnlocked 
            isWeaponUnlocked = weaponStats.isUnlocked;
        }
     


        //  InitializeAmmo();

        // Set gun part positions 
        gunOriginalPosition = gunTransform.localPosition;
        gunTriggerOriginalPosition = trigger.localPosition;
        gunMagOriginalPosition = gunMagTransform.localPosition;
        gunInitialRotation = transform.localRotation;

        //null check and assign gun part positions

        if (gunTransform == null){ }
        if (trigger == null) { }
        if (gunMagTransform == null) { }





    }
    private void Update()
    {
        if (isSwayEnabled)
        {
            ApplySway();
        }
        if (isCamRayLookAtEnabled)
        {
            ApplyGunLookat();
        }
    }


    private void ApplySway()
    {
        // Mouse input 
        float mouseX = Input.GetAxis("Mouse X") * swayAmount;
        float mouseY = Input.GetAxis("Mouse Y") * swayAmount;

        // Clamp sway movement
        mouseX = Mathf.Clamp(mouseX, -maxSwayAmount, maxSwayAmount);
        mouseY = Mathf.Clamp(mouseY, -maxSwayAmount, maxSwayAmount);

        // Calculate new position and rotation then move to target pos
        Vector3 targetPosition = new Vector3(mouseX, mouseY, 0) + gunOriginalPosition;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(-mouseY * rotationSwayAmount, mouseX * rotationSwayAmount, 0)) * gunInitialRotation;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * swaySmoothness);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * swaySmoothness);
    }

    private void ApplyGunLookat()
    {
        // Cast a ray from the camera's position forward
        Ray cameraRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(cameraRay, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = targetPosition - firePoint.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            Debug.DrawLine(cameraRay.origin, hit.point, Color.red);
        }
    }



    protected virtual void FiringSequence()
    {

    }


    public virtual void ReloadingSequence()
    {



    }

    public virtual void Fire()
    {
        OnWeaponFired?.Invoke();


    }


    public virtual void StopFire() {
    
    }

    // Applying upgrade
   public void IncreaseDamage(int amount)
    {
        damage += amount;
    }

    public void IncreaseFireRate(float amount)
    {
        fireRate += amount;
    }
    public void IncreaseAmmoCapacity(int amount)
    {
        ammoCapacity += amount;
    }
}
