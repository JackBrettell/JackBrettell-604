using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    [SerializeField] private float startOverlayHealth = 0.5f; // Health percentage to start showing overlay
    [SerializeField] HUDMediator hudMediator;
    [SerializeField] private Image damageOverlayImageImage;

    public void UpdateDamageOverlay(float currentHealth, float maxHealth)
    {
        // Calculate the opacity based on the player's health
        float healthPercentage = currentHealth / maxHealth;

        if (healthPercentage <= startOverlayHealth)
        {
            // Calculate opacity based on health
            float opacity = 1f - (healthPercentage * 2f);

            // Update the trasnsparency of the overlay
            Color overlayColor = damageOverlayImageImage.color;
            overlayColor.a = Mathf.Clamp(opacity, 0f, 1f);
            damageOverlayImageImage.color = overlayColor;
        }
        else
        {
            // Ensure the overlay is fully transparent when health is above 50%
            Color overlayColor = damageOverlayImageImage.color;
            overlayColor.a = 0f;
            damageOverlayImageImage.color = overlayColor;
        }
        //Debug.Log($"Health Percentage: {healthPercentage}, Overlay Opacity: {damageOverlayImage.color.a}");

    }
}
