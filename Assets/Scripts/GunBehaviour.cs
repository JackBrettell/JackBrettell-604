using DG.Tweening;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public WeaponBase weaponBase;

    [Header("Weapon Settings")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float bulletSpeed = 20f;
    public int damage;
    public int ammoCapacity;
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
    protected bool isRealoading = false;

    public float gunMagMovemement = 0;
    public float gunMagEjectDuration = 0;
    public float gunMagReturnDuration = 0;

    protected bool isReloading = false;
    protected bool isFiring = false;


    public void Start()
    {
        // ===== Auto-assigns =====

        // Weapon base scrip
        weaponBase = GetComponent<WeaponBase>();

        // Set gun part positions 
        gunOriginalPosition = gunTransform.localPosition;
        gunTriggerOriginalPosition = trigger.localPosition;
        gunMagOriginalPosition = gunMagTransform.localPosition;




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
