using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyPopup : MonoBehaviour
{
    public TMP_Text moneyPopupText; // Drag your UI Text here
    public float fadeDuration = 0.5f; // Duration of fade in/out
    public float displayDuration = 1f; // Duration the text stays visible

    private void Start()
    {
        // Subscribe to MoneyManager's event
        MoneyManager.Instance.OnMoneyChanged += ShowMoneyPopup;

        // Hide text initially
        moneyPopupText.alpha = 0;
    }

    private void ShowMoneyPopup(int newAmount)
    {
        // Calculate reward amount (difference between old and new amount)
        int rewardAmount = newAmount - MoneyManager.Instance.CurrentMoney + 10;

        // Set the text to the reward amount
        moneyPopupText.text = $"+{rewardAmount}";

        // Fade in, hold, and fade out
        moneyPopupText.DOFade(1, fadeDuration) // Fade in
            .OnComplete(() =>
            {
                // Wait before fading out
                DOVirtual.DelayedCall(displayDuration, () =>
                {
                    moneyPopupText.DOFade(0, fadeDuration); // Fade out
                });
            });
    }
}
