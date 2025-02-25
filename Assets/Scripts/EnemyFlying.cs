using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyFlying : Enemy, IDamageable
{

    public float flyHeight = 5f;
    private float originalFlyHeight = 0f;
    public float flySpeed = 1f;
    public float followRange = 10f;


    protected override void Awake()
    {
        health = 10;
        damage = 10;

        agent = GetComponent<NavMeshAgent>();


        base.Awake();

        ToggleRagdoll(true);

    }


    protected override void Update()
    {
        FollowPlayer();

        base.Update();
    }

    void FollowPlayer()
    {


        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > followRange)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }

    }



   /* public override void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{damage} damage received. Health: {health}");

        if (health < 0)
        {
            //  Destroy(gameObject);
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            if (animator != null)
            {
                animator.enabled = false;
            }
            else
            {
                Debug.Log("Animator not found!");
            }

            //ToggleKinematics();
            EnemyDeath();

        };
    }*/
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

