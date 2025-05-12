using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;


public interface IDamageable
{
    void TakeDamage(int damage, GameObject part);
}

public class EnemyBase : MonoBehaviour, IDamageable
{
    protected int health = 100;
    protected int damage = 10;
    protected int reward = 10;
    protected float attackSpeed = 1.5f;
    protected float movementSpeed = 3f;
    [SerializeField] protected GameObject player;
    protected Animator animator;
    protected NavMeshAgent agent;


    private Slider healthSlider;

    private EnemyFactory factory;
    public EnemyType enemyType;
    private float timeTilDespawn = 3f;
    [SerializeField] protected float colliderDisableTime = 1f; // Time to disable colliders after death 

    protected Rigidbody[] ragdollBodies;
    [SerializeField] protected Collider[] ragdollColliders;
    protected Collider mainCollider; // NEW: Main collider for preventing player collision

    public delegate void DeathHandler();

    public event System.Action OnDeath; // Event for wave manager to subscribe to
    public static event System.Action OnAnyEnemyKilled; // Separate event for tracking number of enemies killed

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(true);
        ragdollBodies = GetComponentsInChildren<Rigidbody>(true);


        ToggleRagdoll(false);

        healthSlider = GetComponentInChildren<Slider>(true);

        // Initialize values
        healthSlider.maxValue = health;
        healthSlider.value = health;



        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        UpdateHealthbarRotation();

        if (agent.enabled)
        {
            agent.destination = player.transform.position;

        }

    }

    public void Initilize(EnemyFactory enemyFactory, GameObject player)
    {
        factory = enemyFactory;
        this.player = player;
    }

    public virtual void TakeDamage(int damage, GameObject part)
    {

        if (part.name.ToLower().Contains("weak"))
        {
            Debug.Log("Headshot!");
            damage *= 2;

        }
        else
        {
            Debug.Log("Hit!");
        }

        health -= damage;
        UpdateHealthbarValue();


        Debug.Log($"{damage} damage received. Health: {health}");

        if (health <= 0)
        {

            //  Destroy(gameObject);
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            animator.enabled = false;
            EnemyDeath();

        }
    }

    public virtual void DoDamage()
    {
        Debug.Log($"Enemy dealt {damage} damage!");
    }

    public virtual void EnemyDeath()
    {
        //Lock ragdoll position and disable all colliders
        //StartCoroutine(WaitForRagdollToggle());


        Debug.Log("Enemy killed");
        // on death give reward to player
        MoneyManager.Instance.AddMoney(reward);
        OnAnyEnemyKilled?.Invoke();

        // Activate ragdoll
        ToggleRagdoll(true);

        agent.enabled = false;

        // Disable the main collider to prevent player collision
        if (mainCollider != null)
        {
            mainCollider.enabled = false;
        }

       StartCoroutine(Despawn());
    }

    private IEnumerator WaitForRagdollToggle()
    {
        yield return new WaitForSeconds(colliderDisableTime);

        ToggleRagdoll(true);

        //Prevent dead zombies from colliding with player
        foreach (var Colldier in ragdollColliders)
        {
      
            Colldier.enabled = false;
            Debug.Log("Disabled collider" + ragdollColliders  );
        }
        foreach (var rb in ragdollBodies)
        {
            //freeze all rigidbodies
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private IEnumerator Despawn()
    {

        Debug.Log("Despawning enemy");

        yield return new WaitForSeconds(timeTilDespawn);

        ToggleRagdoll(false);
        if (mainCollider != null)
        {
            mainCollider.enabled = true;
        }

        OnDeath?.Invoke();

        factory.ReturnEnemy(enemyType, this);
    }

    protected void UpdateHealthbarRotation()
    {
        healthSlider.transform.LookAt(player.transform.position);
        healthSlider.transform.Rotate(0, 180, 0);
    }

    protected void UpdateHealthbarValue()
    {
        healthSlider.value = health;
    }

    protected void ToggleRagdoll(bool isRagdoll)
    {
        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = !isRagdoll;
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
