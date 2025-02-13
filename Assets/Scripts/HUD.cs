using DG.Tweening;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    private GameObject crosshair;
    public TMP_Text AmmoText;
    private AmmoManager ammoManager;

    [SerializeField] private float crosshairSize = 0;
    [SerializeField] private float crosshairReccoilSize = 0;
    [SerializeField] private float crosshairScaleSpeed = 0;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        ammoManager = GetComponent<AmmoManager>();

    }

    public void crosshairScale()
    {
        Sequence crosshairSequence = DOTween.Sequence();
        crosshairSequence
            .Append(crosshair.transform.DOScale(crosshairReccoilSize, crosshairScaleSpeed))
            .Append(crosshair.transform.DOScale(crosshairSize, crosshairScaleSpeed));
    }

    public void hideHud()
    {
        crosshair.SetActive(false);
    }
    public void showHud()
    {
        crosshair.SetActive(true);
    }

    public void updateAmmoCount()
    {
        if (ammoManager != null)
        {
            AmmoText.text = ammoManager.CurrentAmmo.ToString();

        }
        else { Debug.Log("Current ammo is null"); }
    }

}
