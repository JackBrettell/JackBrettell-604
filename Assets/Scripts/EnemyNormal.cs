using UnityEngine;
using System.Collections;

public class EnemyNormal : EnemyBase, IDamageable
{
    private float lastAttackTime;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxDamage = 10;
    [SerializeField] private int killReward = 10;

    private bool isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        health = maxHealth;
        damage = maxDamage;
        reward = killReward;

        if (mainCollider != null)
            mainCollider.enabled = true;

        if (agent != null)
            agent.isStopped = true;
    }

    protected override void Update()
    {
        base.Update();

        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < attackRange && !isAttacking && Time.time - lastAttackTime > attackCooldown)
        {
            StartCoroutine(Attack());
        }
        else if (distanceToPlayer >= attackRange)
        {
            animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    public void ApplyDamage()
    {
        if (player != null)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }
    }

    public virtual void ToggleKinematics()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !rb.isKinematic;
            Debug.Log("Kinematics toggled");
        }
    }
}
