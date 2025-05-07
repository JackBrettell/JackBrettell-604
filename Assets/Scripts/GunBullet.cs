using System.Collections;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public int damage; 
    private float lifetime = 5f;
    [SerializeField] private HitMarker hitMarker;

    private void Start()
    {
        Destroy(gameObject, lifetime);

    }

    private void OnTriggerEnter(Collider other)
    {
        hitMarker = FindObjectOfType<HitMarker>();
        if (hitMarker == null)
        {
            Debug.LogError("HitMarker not found in the scene.");
            return;
        }

       


        IDamageable damageable = other.gameObject.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            EnemyBase enemy = other.gameObject.GetComponentInParent<EnemyBase>();
            damageable.TakeDamage(damage, other.gameObject);

            hitMarker.ShowHitMarker();
            Debug.Log(other.gameObject.name);
        }
        
        StartCoroutine(DespawnCountdown());
    }

    private IEnumerator DespawnCountdown()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }




}
