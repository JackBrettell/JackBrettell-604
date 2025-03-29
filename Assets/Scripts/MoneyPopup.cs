using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyPopup : MonoBehaviour
{
    public TMP_Text moneyPopupText; 
    public float fadeDuration = 0.5f; 
    public float displayDuration = 1f; 

    private void Start()
    {
     
        MoneyManager.Instance.OnMoneyChanged += ShowMoneyPopup;

        // Hide text 
        moneyPopupText.alpha = 0;
    }

    private void ShowMoneyPopup(int newAmount)
    {
   
        int rewardAmount = newAmount - MoneyManager.Instance.CurrentMoney + 10;
        moneyPopupText.text = $"+{rewardAmount}";

        // Fade in, and fade out
        moneyPopupText.DOFade(1, fadeDuration)
            .OnComplete(() =>
            {
                
                DOVirtual.DelayedCall(displayDuration, () =>
                {
                    moneyPopupText.DOFade(0, fadeDuration); // Fade out
                });
            });
    }
}
