using DG.Tweening;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    private GameObject crosshair;
    public TMP_Text AmmoText;
    public TMP_Text WaveNumText;
    private AmmoManager ammoManager;
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private float crosshairSize = 0;
    [SerializeField] private float crosshairReccoilSize = 0;
    [SerializeField] private float crosshairScaleSpeed = 0;

    [Header("Sway Settings")]
    [SerializeField] private RectTransform hud;                // Use RectTransform for UI
    private Vector2 hudOriginalPosition;                       // Original UI position
    [SerializeField] private float swayAmount = 5f;            // Multiplier for sway
    [SerializeField] private float maxSwayAmount = 20f;        // Limit for sway
    [SerializeField] private float swaySmoothness = 5f;        // Lerp speed
    [SerializeField] private float rotationSwayAmount = 2f;    // Rotation multiplier

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        ammoManager = GetComponent<AmmoManager>();

        if (waveManager != null)
        {
            waveManager.OnRoundFinished += UpdateWaveNumber; // Subscribe to round finished event
        }

        WaveNumText.text = "Wave: 1"; // Set initial wave number
        hudOriginalPosition = hud.anchoredPosition; // Store initial HUD position
    }

    private void Update()
    {
        ApplySway();



    }

    public void crosshairScale()
    {
        Sequence crosshairSequence = DOTween.Sequence();
        crosshairSequence
            .Append(crosshair.transform.DOScale(crosshairReccoilSize, crosshairScaleSpeed))
            .Append(crosshair.transform.DOScale(crosshairSize, crosshairScaleSpeed));
    }

    private void UpdateWaveNumber()
    {
        int currentWave = waveManager.currentWaveIndex + 2;
        WaveNumText.text = "Wave: " + currentWave;
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
        else
        {
            Debug.Log("Current ammo is null");
        }
    }

    private void ApplySway()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate sway based on mouse movement
        Vector2 sway = new Vector2(mouseX, mouseY) * swayAmount;

        // Clamp the sway to prevent excessive movement
        sway.x = Mathf.Clamp(sway.x, -maxSwayAmount, maxSwayAmount);
        sway.y = Mathf.Clamp(sway.y, maxSwayAmount, -maxSwayAmount + -50);

        // Apply sway to the HUD anchored position
        Vector2 targetPosition = hudOriginalPosition + sway;
        hud.anchoredPosition = Vector2.Lerp(hud.anchoredPosition, targetPosition, Time.deltaTime * swaySmoothness);

        // Rotation sway
        Vector3 targetRotation = new Vector3(-mouseY * rotationSwayAmount, mouseX * rotationSwayAmount, 0);
        hud.localRotation = Quaternion.Lerp(hud.localRotation, Quaternion.Euler(targetRotation), Time.deltaTime * swaySmoothness);




    }
}
