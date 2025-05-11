using UnityEngine;
using DG.Tweening;
public class HitMarker : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float scaleToSize = 0.5f;
    [SerializeField] private CanvasGroup hitMarkerCanvasGroup;


    public void ShowHitMarker()
    {
        hitMarker.SetActive(true);

        // dotween sequence to fade in and then out
        // Scale hit marker from 0
        Sequence sequence = DOTween.Sequence();
        sequence.Append(hitMarkerCanvasGroup.DOFade(1, fadeDuration))
                .Join(hitMarker.transform.DOScale(scaleToSize, fadeDuration))
                .Append(hitMarkerCanvasGroup.DOFade(0, fadeDuration))
                .Join(hitMarker.transform.DOScale(0, fadeDuration))
                .OnComplete(() => hitMarker.SetActive(false));


    }
}