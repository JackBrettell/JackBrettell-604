using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;


public interface IDamageable
{
    void TakeDamage(float damage, GameObject part);
}

public class EnemyBase : MonoBehaviour, IDamageable
{
    protected float health = 100;
    protected int damage = 10;
    protected int reward = 10;
    protected float attackSpeed = 1.5f;
    protected float movementSpeed = 3f;
    protected float criticalDamage = 2f;
    [SerializeField] protected GameObject player;
    protected Animator animator;
    protected NavMeshAgent agent;
    private Vector3 lastPlayerPosition;
    private float updateThreshold = 1f; 

    private Slider healthSlider;

    private EnemyFactory factory;
    public EnemyType enemyType;
    private float timeTilDespawn = 3f;

    protected Rigidbody[] ragdollBodies;
    [SerializeField] protected Collider[] ragdollColliders;
    protected Collider mainCollider;

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
        lastPlayerPosition = player.transform.position;
    }

    protected virtual void Update()
    {
        UpdateHealthbarRotation();

        if (agent.enabled)
        {
            float distance = Vector3.Distance(player.transform.position, lastPlayerPosition);
            if (distance > updateThreshold)
            {
                agent.destination = player.transform.position;
                lastPlayerPosition = player.transform.position;
            }
        }
    }

    public void Initialize(EnemyFactory enemyFactory, GameObject player)
    {
        factory = enemyFactory;
        this.player = player;
    }

    public virtual void TakeDamage(float damage, GameObject part)
    {

        if (part.name.ToLower().Contains("weak"))
        {
            damage *= criticalDamage;
        }
 
        health -= damage;
        UpdateHealthbarValue();

        if (health <= 0)
        {

            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            animator.enabled = false;
            EnemyDeath();

        }
    }

    public virtual void DoDamage()
    {
    }

    public virtual void EnemyDeath()
    {
        // Disable health bar
        healthSlider.gameObject.SetActive(false);

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

    private IEnumerator Despawn()
    {


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