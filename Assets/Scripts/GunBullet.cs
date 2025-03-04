using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public int damage; 
    private float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            EnemyBase enemy = collision.gameObject.GetComponentInParent<EnemyBase>();
            damageable.TakeDamage(damage, collision.gameObject);
            Debug.Log(collision.gameObject.name);
        }

        Destroy(gameObject);
    }
}
