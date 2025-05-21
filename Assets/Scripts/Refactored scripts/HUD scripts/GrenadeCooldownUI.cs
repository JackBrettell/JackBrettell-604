using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GrenadeCooldownUI : MonoBehaviour
{
    [SerializeField] private Image cooldownFill;
    [SerializeField] private Image grenadeIcon;

    private float totalCooldownDuration = 5f;
    [SerializeField] private Color startColor = Color.red;
    [SerializeField] private Color endColor = Color.white;

    [Header("Finished animation")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Ease animationEase = Ease.OutBack;
    private float iconStartSize = 1f;
    [SerializeField] private float iconEndSize = 1.5f;
    [SerializeField] private float iconFailEndSize= 1.1f;
    [SerializeField] private float animationFailDuration = 0.5f;

    private void Start()
    {
        iconStartSize = grenadeIcon.transform.localScale.x;
    }

    public void UpdateCoolDownBar(float remainingCooldown, float totalDuration)
    {
        totalCooldownDuration = totalDuration;

        float progress = 1f - Mathf.Clamp01(remainingCooldown / totalCooldownDuration);
        cooldownFill.fillAmount = progress;

        cooldownFill.color = Color.Lerp(startColor, endColor, progress);
        grenadeIcon.color = Color.Lerp(startColor, endColor, progress);

        if (remainingCooldown <= 0f)
            AnimateIcon();
    }


    // When the grenade cooldown is finished
    private void AnimateIcon()
    {
        cooldownFill.transform.DOScale(iconEndSize, animationDuration)
            .SetEase(animationEase)
            .OnStart(() =>
            {
                cooldownFill.color = Color.yellow;
                grenadeIcon.color = Color.yellow;
            })
            .OnComplete(() =>
            {
                cooldownFill.transform.DOScale(iconStartSize, animationDuration)
                    .SetEase(animationEase);
                cooldownFill.color = Color.white;
                grenadeIcon.color = Color.white;
            });
    }

    // When grenade is used whilt on cooldown
    public void AnimateFailIcon()
    {
        cooldownFill.transform.DOScale(iconFailEndSize, animationFailDuration)
            .SetEase(animationEase)
            .OnStart(() =>
            {
                cooldownFill.color = startColor;
            })
            .OnComplete(() =>
            {
                cooldownFill.transform.DOScale(iconStartSize, animationFailDuration)
                    .SetEase(animationEase);
                cooldownFill.color = Color.white;
                grenadeIcon.color = Color.white;
            });

    }
}
