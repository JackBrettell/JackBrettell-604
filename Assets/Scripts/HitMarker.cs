using UnityEngine;
using DG.Tweening;
public class HitMarker : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float fadeDuration = 0.5f;


    public void ShowHitMarker()
    {
        hitMarker.SetActive(true);

        // dotween sequence to fade in and then out
        Sequence hitMarkerSequence = DOTween.Sequence();
        hitMarkerSequence
            .Append(hitMarker.GetComponent<CanvasGroup>().DOFade(1, fadeDuration))
            .AppendInterval(0.5f)
            .Append(hitMarker.GetComponent<CanvasGroup>().DOFade(0, fadeDuration))
            .OnComplete(() => hitMarker.SetActive(false)); // Deactivate after fading out
    }
}