using DG.Tweening;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private GameObject crosshair;
    [SerializeField] private float crosshairSize = 0;
    [SerializeField] private float crosshairReccoilSize = 0;
    [SerializeField] private float crosshairScaleSpeed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crosshair = GameObject.Find("Crosshair");

    }

    public void crosshairScale()
    {

        Sequence crosshairSequence = DOTween.Sequence();

        crosshairSequence
            .Append(crosshair.transform.DOScale(crosshairReccoilSize, crosshairScaleSpeed))
            .Append(crosshair.transform.DOScale(crosshairSize, crosshairScaleSpeed));


    }

}
