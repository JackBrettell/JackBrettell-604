using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class AmmoCountUI : MonoBehaviour
{
    [SerializeField] private Image ammoValueFill;
    [SerializeField] private Image ammoIcon;
    [SerializeField] private Color lowAmmoColour = Color.red;
    [SerializeField] private Color highAmmoColour = Color.white;
    [SerializeField] private float startColourChange = 0.5f;

    [Header("Animation")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Ease animationEase = Ease.OutBack;
    private float iconStartSize = 1f;
    [SerializeField] private float iconEndSize = 1.5f;
    private Tween currentTween;
   // private bool hasReloaded = false;

    private void Start()
    {
        iconStartSize = ammoValueFill.transform.localScale.x;
    }
    public void UpdateAmmoBar(int currentAmmo, int maxAmmo)
    {
        float progress = (float)currentAmmo / maxAmmo;
        ammoValueFill.fillAmount = progress;

        if (progress >= startColourChange)
        {
            // No colour change until startColourChange is met (%50)
            ammoValueFill.color = highAmmoColour;
        }
        else
        {
            float lerpProgress = Mathf.InverseLerp(0f, 0.5f, progress);
            ammoValueFill.color = Color.Lerp(lowAmmoColour, highAmmoColour, lerpProgress);

            if (currentAmmo == 0)
            {
                // Play no ammo animation
                NoAmmoAnimation();
            }
        }

        // Play reloaded animation
        if (currentAmmo == maxAmmo)
        {
          //  hasReloaded = true;
            ReloadedAnimation();
        }
    }


    private void NoAmmoAnimation()
    {
        // Kill any running animation to prevent interference
        currentTween?.Kill();

        // Scale up
        currentTween = ammoValueFill.transform.DOScale(iconEndSize, animationDuration)
            .SetEase(animationEase)
            .OnStart(() =>
            {
                // Fill bar fully and go red
                ammoValueFill.fillAmount = 1f;
                ammoValueFill.color = lowAmmoColour;
                ammoIcon.color = lowAmmoColour;
            })
            .OnComplete(() =>
            {
                // Scale back down
                currentTween = ammoValueFill.transform.DOScale(iconStartSize, animationDuration)
                    .SetEase(animationEase)
                    .OnComplete(() =>
                    {
                        // Fully reset only after both tweens are done
                        ammoValueFill.fillAmount = 0f;
                        ammoValueFill.color = highAmmoColour;
                        ammoIcon.color = highAmmoColour;
                    });
            });
    }
    private void ReloadedAnimation()
    {
        // Kill any running animation to prevent interference
        currentTween?.Kill();

        // Scale up
        currentTween = ammoValueFill.transform.DOScale(iconEndSize, animationDuration)
            .SetEase(animationEase)
            .OnStart(() =>
            {
            })
            .OnComplete(() =>
            {
                // Scale back down
                currentTween = ammoValueFill.transform.DOScale(iconStartSize, animationDuration)
                    .SetEase(animationEase)
                    .OnComplete(() =>
                    {
                        // Fully reset only after both tweens are done
                        ammoValueFill.color = highAmmoColour;
                        ammoIcon.color = highAmmoColour;
                        
                    });
            });
    }


}
