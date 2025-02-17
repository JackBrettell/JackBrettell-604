using UnityEngine;
using System.Collections;
public interface IDamageable
{
    void TakeDamage(int damage);
}

public class Enemy : MonoBehaviour, IDamageable
{
    protected int health = 100;
    protected int damage = 10;
    protected float attackSpeed = 1.5f;
    protected float movementSpeed = 3f;

    private EnemyFactory factory;
    public EnemyType enemyType;
    private float timeTilDespawn = 3f;

    private Animator animator;
    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Collider mainCollider; // NEW: Main collider for preventing player collision

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    private void Awake()
    {
        factory = FindFirstObjectByType<EnemyFactory>();
        animator = GetComponentInChildren<Animator>();

        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        Debug.Log("Ragdoll Bodies Found: " + ragdollBodies.Length);


        mainCollider = GetComponent<Collider>(); // Store main collider






        ToggleRagdoll(false);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{damage} damage received. Health: {health}");

        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    public virtual void DoDamage()
    {
        Debug.Log($"Enemy dealt {damage} damage!");
    }

    public virtual void EnemyDeath()
    {
        Debug.Log("Enemy killed");

        // Activate ragdoll
        ToggleRagdoll(true);

        // Disable the main collider to prevent player collision
        if (mainCollider != null)
        {
            mainCollider.enabled = false;
        }

        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeTilDespawn);

        // Deactivate ragdoll
        ToggleRagdoll(false);

        // Re-enable the main collider
        if (mainCollider != null)
        {
            mainCollider.enabled = true;
        }

        // Notify wave manager
        OnDeath?.Invoke();

        // Return to pool
        factory.ReturnEnemy(enemyType, gameObject);
    }

    private void ToggleRagdoll(bool isRagdoll)
    {
        if (ragdollBodies == null || ragdollBodies.Length == 0)
        {
            ragdollBodies = GetComponentsInChildren<Rigidbody>();
            ragdollColliders = GetComponentsInChildren<Collider>();
            mainCollider = GetComponent<Collider>();
        }

        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = !isRagdoll;
            Debug.Log($"Rigidbody {rb.name} is kinematic: {rb.isKinematic}");
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = isRagdoll;
        }

        if (animator != null)
        {
            animator.enabled = !isRagdoll;
        }
    }

}
