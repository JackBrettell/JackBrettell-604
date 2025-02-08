using DG.Tweening;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public WeaponBase weaponBase;
    public AmmoManager ammoManager;
    public HUD hud;


    [Header("Weapon Settings")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float bulletSpeed = 20f;
    public int damage;
    [SerializeField] protected int ammoCapacity = 30;

    public float fireRate;
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
    private Quaternion gunInitialRotation;
    protected bool isRealoading = false;

    public float gunMagMovemement = 0;
    public float gunMagEjectDuration = 0;
    public float gunMagReturnDuration = 0;

    protected bool isReloading = false;
    protected bool isFiring = false;

    [Header("Sway Settings")]
    [SerializeField] private float swayAmount = 0.02f;      // How much the gun moves
    [SerializeField] private float maxSwayAmount = 0.06f;   // Max movement limit
    [SerializeField] private float swaySmoothness = 5f;     // Damping effect
    [SerializeField] private float rotationSwayAmount = 4f; // Rotation amount



    public virtual void Start()
    {
        // ===== Auto-assigns =====

        // Weapon base scrip
        //  weaponBase = GetComponent<WeaponBase>();
        //  ammoManager = GetComponent<AmmoManager>();

        if (ammoManager == null)
            ammoManager = GetComponent<AmmoManager>();

        InitializeAmmo();

        // Set gun part positions 
        gunOriginalPosition = gunTransform.localPosition;
        gunTriggerOriginalPosition = trigger.localPosition;
        gunMagOriginalPosition = gunMagTransform.localPosition;
        gunInitialRotation = transform.localRotation;




    }
    private void Update()
    {
        ApplySway();
    }

    private void ApplySway()
    {
        // Get mouse input 
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

    public void InitializeAmmo()
    {
   
            ammoManager.Initialize(ammoCapacity);
    }

    public virtual void FiringSequence()
    {

    }


    public virtual void ReloadingSequence()
    {



    }

    public virtual void Fire()
    {

    }

    public virtual void StopFire() {
    
    }
}
