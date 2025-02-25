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
    }


    public void Update()
    {
        FollowPlayer();

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

        Vector3 targetPosition = agent.destination;


        // Apply a height offset
        targetPosition.y = flyHeight;



    }



    public override void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{damage} damage received. Health: {health}");

        if (health < 0)
        {
            //  Destroy(gameObject);
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            animator.enabled = false;

            //ToggleKinematics();
            EnemyDeath();

        };
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

