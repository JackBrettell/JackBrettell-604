using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private GameObject crosshair;
    [SerializeField] private WaveManager waveManager;



    [Header("Money")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private MoneyManager moneyManager;


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

        if (waveManager != null)
        {
          //  waveManager.OnRoundFinished += UpdateWaveNumber; // Subscribe to round finished event
        }

        hudOriginalPosition = hud.anchoredPosition; // Store initial HUD position



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





    public void ShopToggled(bool isShopOpened)
    {
        if (isShopOpened)
        {
            crosshair.SetActive(false);
        }
        else
        {
            crosshair.SetActive(true);
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


    public void UpdateMoney(int newAmount)
    {
        moneyText.text = $"${newAmount}"; // Format to show money with $
    }


}
