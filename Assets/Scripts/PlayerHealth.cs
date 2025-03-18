using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] private float regenAmount = 5f; // Amount healed per second
    [SerializeField] private float regenDelay = 5f;  // Time before regen starts
    [SerializeField] private float regenInterval = 0.5f; // How often to regenerate
    [SerializeField] private HUD hud;

    private Coroutine regenCoroutine;
    private bool isRegenerating = false;

    private void Update()
    {
        // For testing, press K to take damage
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        hud.UpdateHealthBar();

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage! Current health: {currentHealth}");

        // Update health bar after taking damage
        hud.UpdateHealthBar();


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
        Debug.Log("Player has died");
        // Handle death logic here
    }

    private IEnumerator HealthRegen()
    {
        Debug.Log("Starting health regen...");

        yield return new WaitForSeconds(regenDelay);

        isRegenerating = true;

        while (currentHealth < maxHealth && isRegenerating)
        {
            Heal(regenAmount);
            yield return new WaitForSeconds(regenInterval);
        }

        isRegenerating = false;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log($"Player healed! Current health: {currentHealth}");

        // Update health bar after healing
        hud.UpdateHealthBar();
    }


}
