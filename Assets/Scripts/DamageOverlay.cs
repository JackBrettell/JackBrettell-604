using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float startOverlayHealth = 0.5f; // Health percentage to start showing overlay
    private Image damageOverlayImage;

    private void Start()
    {
        damageOverlayImage = GetComponent<Image>();
    }
    private void Update()
    {
        // Calculate the opacity based on the player's health
        float healthPercentage = playerHealth.CurrentHealth / playerHealth.maxHealth;

        if (healthPercentage <= startOverlayHealth)
        {
            // Calculate opacity based on health
            float opacity = 1f - (healthPercentage * 2f);

            // Update the trasnsparency of the overlay
            Color overlayColor = damageOverlayImage.color;
            overlayColor.a = Mathf.Clamp(opacity, 0f, 1f);
            damageOverlayImage.color = overlayColor;
        }
        else
        {
            // Ensure the overlay is fully transparent when health is above 50%
            Color overlayColor = damageOverlayImage.color;
            overlayColor.a = 0f;
            damageOverlayImage.color = overlayColor;
        }
        //Debug.Log($"Health Percentage: {healthPercentage}, Overlay Opacity: {damageOverlayImage.color.a}");


    }
}
