using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Rendering;
using DG.Tweening;

public class EnemyNormal : EnemyBase, IDamageable
{
    private float lastAttackTime;
    [SerializeField] private float attackCooldown = 1f; // Time in seconds between attacks
    [SerializeField] private float attackRange = 2f; // Distance at which the enemy can attack


    protected override void Awake()
    {
        health = 100;
        damage = 10;
        reward = 10;

        base.Awake();

        mainCollider = GetComponent<Collider>(); // Store main collider
        mainCollider.enabled = true;

        agent.isStopped = true;
    }

    private bool isAttacking = false;

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
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
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;

        // Wait for the animation tro finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }



  







public void ApplyDamage()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);

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

