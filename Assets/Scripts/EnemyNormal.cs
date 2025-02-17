using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyNormal : Enemy, IDamageable
{
    private Slider healthSlider;
    private GameObject healthSliderObj;
    private GameObject player;
    private Animator animator;



    private void Awake()
    {
        health = 10;
        damage = 10;

        player = GameObject.Find("Player");
        healthSliderObj = GameObject.Find("HealthBar");
        healthSlider = healthSliderObj.GetComponent<Slider>();

        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.Log("Animator not found");
        }

        // Initialize values
        healthSlider.maxValue = health;
        healthSlider.value = health;

    }

    public void Update()
    {
        healthSlider.value = health;
        healthSliderObj.transform.LookAt(player.transform.position);
        healthSliderObj.transform.Rotate(0, 180, 0);

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

