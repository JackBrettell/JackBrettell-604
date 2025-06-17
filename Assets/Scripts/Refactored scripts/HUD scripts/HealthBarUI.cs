using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;

    [SerializeField] private HUDMediator hudMediator;


    public void InitialiseHealthUI(float maxHealth, float currentHealth)
    {
        // Initialize health bar
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

    }

    public void UpdateHealthBar(float currentHealth)
    {
        healthBar.value = currentHealth;
    }
}
