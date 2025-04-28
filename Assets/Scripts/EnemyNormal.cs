using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyNormal : EnemyBase, IDamageable
{
    private float lastAttackTime;
    private float attackCooldown = 1f; // Time in seconds between attacks


    protected override void Awake()
    {
        health = 100;
        damage = 10;
        reward = 10;

        base.Awake();

        mainCollider = GetComponent<Collider>(); // Store main collider
        mainCollider.enabled = true;
    }
 
    protected override void Update()
    {
        base.Update();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < 2 && Time.time >= lastAttackTime + attackCooldown)
            {
                agent.isStopped = true; 
                StartCoroutine(Attack());
                lastAttackTime = Time.time;
            }
            else if (distanceToPlayer >= 2)
            {
                animator.SetTrigger("Walk");
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }
        }
    }



    private IEnumerator Attack()
    {

        animator.SetTrigger("Attack");
        agent.isStopped = true;

        yield return new WaitForSeconds(0);

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

