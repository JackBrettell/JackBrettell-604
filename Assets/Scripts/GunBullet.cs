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
        hitMarker = FindObjectOfType<HitMarker>();

    }

    private void Update()
    {
        //debug line to show bullet direction
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
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
