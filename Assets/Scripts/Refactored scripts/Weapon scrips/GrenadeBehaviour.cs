using UnityEngine;
using System.Collections;
public class GrenadeBehaviour : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f; // Radius of the explosion
    [SerializeField] private float explosionForce = 700f; // Force of the explosion
    [SerializeField] private int explosionDamage = 50; // Damage dealt by the explosion
    [SerializeField] private float detenationTime = 3;
    private ParticleSystem explosionEffect; // Explosion effect prefab
    private Rigidbody rb; // Rigidbody component of the grenade

    private void Awake()
    {
        StartCoroutine(DetenationCountdown());
        explosionEffect = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the grenade has collided with something
        if (collision.relativeVelocity.magnitude > 1f)
        {
            //make grenade heavy
           // rb.isKinematic = true;
        }
    }

    private IEnumerator DetenationCountdown()
    {

        Debug.Log("Grenade will explode in " + detenationTime + " seconds.");
        yield return new WaitForSeconds(detenationTime);
        Explode();
    }

    private void Explode()
    {
        explosionEffect.Play();
        Destroy(gameObject, explosionEffect.main.duration);

        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // Check if the collider has a Rigidbody component
            Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                // Add explosion force to the rigidbody
                targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            // Check if the collider has an IDamageable component
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                // Deal damage to the object
                damageable.TakeDamage(explosionDamage, gameObject);
            }
        }

    }
}
