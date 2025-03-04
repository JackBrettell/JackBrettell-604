using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyFlying : EnemyBase, IDamageable
{

    public float flyHeight = 5f;
    private float originalFlyHeight = 0f;
    public float flySpeed = 1f;
    public float followRange = 10f;

    public bool isInRange = false;


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

   private void FollowPlayer()
    {


        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > followRange)
        {
            isInRange = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            isInRange = true;
            agent.SetDestination(transform.position);
            SwoopingAttack();
        }

    }

    private void SwoopingAttack()
    {
        
        Vector3 playerPosition = player.transform.position;

  
        Vector3 swoopDownPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

     
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Vector3 oppositeSidePosition = playerPosition + directionToPlayer * followRange; 

      
        oppositeSidePosition.y = flyHeight;

        
        Sequence swoopSequence = DOTween.Sequence();

        swoopSequence
            .Append(transform.DOMove(swoopDownPosition, 1f).SetEase(Ease.InOutSine)) 
            .Append(transform.DOMove(oppositeSidePosition, 1f).SetEase(Ease.OutSine)) 
            .Play();
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

