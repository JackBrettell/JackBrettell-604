using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private GameObject crosshair;
    public TMP_Text AmmoText;
    public TMP_Text WaveNumText;
    private AmmoManager ammoManager;
    [SerializeField] private WaveManager waveManager;

    [Header("Health Bar")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider healthBar;

    [Header("Money")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private MoneyManager moneyManager;


    [Header("Intermission")]
    [SerializeField] private GameObject intermissionUI;
    [SerializeField] private TMP_Text intermissionCountdown;
    

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
          //  waveManager.OnRoundFinished += UpdateWaveNumber; // Subscribe to round finished event
        }

        WaveNumText.text = "Wave: 1"; // Set initial wave number
        hudOriginalPosition = hud.anchoredPosition; // Store initial HUD position

        // Initialize health bar
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.CurrentHealth;

        // 🎉 Subscribe to money update event
        if (moneyManager != null)
        {
            moneyManager.OnMoneyChanged += UpdateMoney;
            UpdateMoney(moneyManager.CurrentMoney); // Initialize with current money
        }
    }

    private void Update()
    {
        ApplySway();

    }

  /*  public void crosshairScale()
    {
        Sequence crosshairSequence = DOTween.Sequence();
        crosshairSequence
            .Append(crosshair.transform.DOScale(crosshairReccoilSize, crosshairScaleSpeed))
            .Append(crosshair.transform.DOScale(crosshairSize, crosshairScaleSpeed));
    }*/

    private void UpdateWaveNumber()
    {
       // int currentWave = waveManager.currentWaveIndex + 2;
       // WaveNumText.text = "Wave: " + currentWave;
    }



    // Intermission
    public void hideIntermission() { intermissionUI.SetActive(false); }
    public void showIntermission() { intermissionUI.SetActive(true); }

    public void ShopToggled(bool isShopOpened)
    {
        if (isShopOpened)
        {
            crosshair.SetActive(false);
            AmmoText.gameObject.SetActive(false);
        }
        else
        {
            crosshair.SetActive(true);
            AmmoText.gameObject.SetActive(true);
        }
        

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

    public void StartIntermissionTimer(int duration)
    {
        StartCoroutine(IntermissionCountdown(duration));
    }

    private IEnumerator IntermissionCountdown(int duration)
    {
        for (int timeLeft = duration; timeLeft >= 0; timeLeft--)
        {
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;

            intermissionCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1f);
        }

        intermissionCountdown.text = "00:00"; 
    }

    public void UpdateHealthBar()
    {
        healthBar.value = playerHealth.CurrentHealth;
    }
    public void UpdateMoney(int newAmount)
    {
        moneyText.text = $"${newAmount}"; // Format to show money with $
    }


}
