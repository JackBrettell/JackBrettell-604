using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    public float maxHealth = 100f;
    public float CurrentHealth => currentHealth;
    private float currentHealth;

    [SerializeField] private float regenAmount = 5f; // Amount healed per second
    [SerializeField] private float regenDelay = 5f;  // Time before regen 
    [SerializeField] private float regenInterval = 0.5f; // How often to regen
    [SerializeField] private HUD hud;

    // OnDeath event
    public event System.Action<bool> OnDeath;
    public Action<float, float > OnInitialiseHealthbar;
    public Action<float> OnHealthChanged;

    public Action<float, float> OnUpdateHealthOverlay;

    private Coroutine regenCoroutine;
    private bool isRegenerating = false;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(10);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        OnInitialiseHealthbar?.Invoke(maxHealth, currentHealth);
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthChanged();

        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            // Stop and reset health regen if the player is hit again
            if (regenCoroutine != null)
            {
                StopCoroutine(regenCoroutine);
                isRegenerating = false;
            }

            // Start health regen after a delay
            regenCoroutine = StartCoroutine(HealthRegen());
        }
    }

    public void Death()
    {
        bool died = true;
        Debug.Log("Player has died");
        OnDeath?.Invoke(died);
    }

    private IEnumerator HealthRegen()
    {
        yield return new WaitForSeconds(regenDelay);

        isRegenerating = true;

        while (currentHealth < maxHealth && isRegenerating)
        {
            // Smoothly regenerate health over time
            float targetHealth = Mathf.Min(currentHealth + regenAmount, maxHealth);
            float regenSpeed = regenAmount / regenInterval; // Speed of regeneration per second

            while (currentHealth < targetHealth && isRegenerating)
            {
                currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, regenSpeed * Time.deltaTime);
                HealthChanged();
                yield return null; // Wait for the next frame
            }
        }

        isRegenerating = false;
    }

    private void HealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth);
        OnUpdateHealthOverlay?.Invoke(currentHealth, maxHealth);
    }


    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }


     
    }


}
