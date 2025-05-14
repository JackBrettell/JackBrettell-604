using UnityEngine;
using DG.Tweening;
using System;
using System.ComponentModel.Design.Serialization;
public class CrosshairUI : MonoBehaviour
{
    [Header("Hitmarker Settings")]
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float scaleToSize = 0.5f;
    [SerializeField] private CanvasGroup hitMarkerCanvasGroup;

    [Header("Crosshair Settings")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float crosshairScaleSpeed = 1f;
    [SerializeField] private float crosshairStartSize = 1f;
    [SerializeField] private float crosshairEndSize = 1f;



    
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


    // Crosshair reccoil
    public void crosshairScale()
    {
        Sequence crosshairSequence = DOTween.Sequence();
        crosshairSequence
            .Append(crosshair.transform.DOScale(crosshairEndSize, crosshairScaleSpeed))
            .Append(crosshair.transform.DOScale(crosshairStartSize, crosshairScaleSpeed));
    }
}