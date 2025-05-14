using System;
using System.Collections;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public int damage;
    private float lifetime = 5f;
    public Action OnEnemyHit;

    private Coroutine lifetimeCoroutine;

    private void OnEnable()
    {
        lifetimeCoroutine = StartCoroutine(AutoDespawn());
    }

    private void OnDisable()
    {
        if (lifetimeCoroutine != null)
        {
            StopCoroutine(lifetimeCoroutine);
            lifetimeCoroutine = null;
        }
    }

    private IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(lifetime);
        Despawn();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, other.gameObject);
            OnEnemyHit?.Invoke();
        }

        StartCoroutine(HitDespawn());
    }

    private IEnumerator HitDespawn()
    {
        yield return new WaitForSeconds(0.25f);
        Despawn();
    }

    private void Despawn()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}
