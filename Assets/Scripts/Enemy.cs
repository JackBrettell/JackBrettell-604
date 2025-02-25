using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;


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
    protected GameObject player;
    protected Animator animator;
    protected NavMeshAgent agent;


    private Slider healthSlider;
    private GameObject healthSliderObj;

    private EnemyFactory factory;
    public EnemyType enemyType;
    private float timeTilDespawn = 3f;


    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Collider mainCollider; // NEW: Main collider for preventing player collision

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    protected virtual void Awake()
    {
        factory = FindFirstObjectByType<EnemyFactory>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");


        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();



        mainCollider = GetComponent<Collider>(); // Store main collider

        ToggleRagdoll(false);


        healthSliderObj = GameObject.Find("HealthBar");
        healthSlider = healthSliderObj.GetComponent<Slider>();

        // Initialize values
        healthSlider.maxValue = health;
        healthSlider.value = health;

        mainCollider.enabled = true;

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
        healthSlider.value = health;
        healthSliderObj.transform.LookAt(player.transform.position);
        healthSliderObj.transform.Rotate(0, 180, 0);
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
