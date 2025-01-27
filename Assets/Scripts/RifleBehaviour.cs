using DG.Tweening;
using TMPro;
using UnityEngine;

public class RifleBehaviour : MonoBehaviour
{
    public WeaponBase weaponBase;

    [Header("Weapon Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    public int damage;
    public int ammoCapacity;
    public float fireRate;
    private Transform firePoint;

    [Header("Recoil")]
    [SerializeField] private float recoilAmount = 0.1f;
    [SerializeField] private float recoilUpAmount = 0.1f;
    [SerializeField] private float recoilRecoverySpeed = 5f;
    [SerializeField] private float recoilDuration = 0;
    private Vector3 rifleOriginalPosition;
    private Transform rifleTransform;
    private Ease Ease1 = Ease.Linear;
    private Ease Ease2 = Ease.Linear;

    [Header("Trigger")]
    [SerializeField] private float triggerDownDuration = 0f;
    [SerializeField] private float triggerRecoveryDuration = 0f;
    private Vector3 rifleTriggerOriginalPosition;
    private Transform trigger;
    private Vector3 triggerDownRotation = new Vector3(45, 0, 0);

    [Header("Reload")]
    public Ease EaseReload = Ease.InBounce;
    public float reloadMovement = 0.01f;
    public float reloadMovementUp = 0.01f;
    public float reloadDuration = 1;
    public float reloadReturnDuration = 1;

    private Transform rifleMagTransform;
    private Vector3 rifleMagOriginalPosition;
    private bool isRealoading = false;

    public float rifleMagMovemement = 0;
    public float rifleMagEjectDuration = 0;
    public float rifleMagReturnDuration = 0;



    public void Start()
    {
        // ===== Auto-assigns =====

        // Weapon base scrip
        weaponBase = GetComponent<WeaponBase>();


        // Fire point
        GameObject firePointObject = GameObject.Find("rifle_muzzle");
        firePoint = firePointObject.transform;

        // Weapon
        GameObject gunTransform = GameObject.Find("Rifle");
        rifleTransform = gunTransform.transform;

        // Trigger
        GameObject triggerTransform = GameObject.Find("rifle_trigger");
        trigger = triggerTransform.transform;

        // Magazine
        GameObject magTransform = GameObject.Find("rifle_mag");
        rifleMagTransform = magTransform.transform;



        // Set gun part positions 
        rifleOriginalPosition = rifleTransform.localPosition;
        rifleTriggerOriginalPosition = trigger.localPosition;
        rifleMagOriginalPosition = rifleMagTransform.localPosition;




    }

    public void FiringSequence()
    {
        Sequence firingSequence = DOTween.Sequence();
        Vector3 recoilOffset = Vector3.back * recoilAmount + Vector3.up * recoilUpAmount;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's damage
        RifleBullet bulletScript = bullet.GetComponent<RifleBullet>();
        bulletScript.damage = damage;
       

        // Add velocity to the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = firePoint.forward * bulletSpeed;

        firingSequence
            .Join(rifleTransform.DOLocalMove(rifleOriginalPosition + recoilOffset, recoilDuration, false).SetEase(Ease1))
            .Append(rifleTransform.DOLocalMove(rifleOriginalPosition, recoilRecoverySpeed, false));
    }


    public void ReloadingSequence()
    {

        Vector3 reloadOffset = Vector3.right * reloadMovement + Vector3.up * reloadMovementUp;
        Vector3 reloadRotation = new Vector3(0, 0, -25);
        Vector3 reloadRotationUp = new Vector3(-40, 0, 0);

        Vector3 rifleMagOffsetDowm = rifleMagOriginalPosition + Vector3.down * rifleMagMovemement;
        Vector3 rifleMagOffsetBack = rifleMagOriginalPosition + Vector3.back * rifleMagMovemement;



        Sequence reloadingSequence = DOTween.Sequence();

        reloadingSequence
                .Join(rifleTransform.DOLocalMove(rifleOriginalPosition + reloadOffset, reloadDuration).SetEase(EaseReload))
                .Join(rifleTransform.DOLocalRotate(reloadRotation, reloadDuration, RotateMode.Fast).SetEase(EaseReload))
                .Join(rifleTransform.DOLocalRotate(reloadRotationUp, reloadDuration, RotateMode.Fast).SetEase(EaseReload))
                // .AppendInterval(1f)
                // Lower magazine
                .Append(rifleMagTransform.DOLocalMove(rifleMagOffsetDowm, rifleMagEjectDuration).SetEase(Ease.Linear))
                .Append(rifleMagTransform.DOLocalMove(rifleMagOffsetBack, rifleMagEjectDuration).SetEase(Ease.Linear))


                // Return magazine
                .Append(rifleMagTransform.DOLocalMove(rifleMagOffsetDowm, rifleMagReturnDuration).SetEase(Ease.Linear))
                .Append(rifleMagTransform.DOLocalMove(rifleMagOriginalPosition, rifleMagReturnDuration).SetEase(Ease.Linear))

                .Append(rifleTransform.DOLocalMove(rifleOriginalPosition, reloadReturnDuration).SetEase(EaseReload))
                .Join(rifleTransform.DOLocalRotate(Vector3.zero, reloadReturnDuration, RotateMode.Fast).SetEase(EaseReload))
                .OnComplete(() =>
                {
                    weaponBase.isRifleReloading = false;

                });

        weaponBase.rifleAmmoCount = ammoCapacity;

    }
}
