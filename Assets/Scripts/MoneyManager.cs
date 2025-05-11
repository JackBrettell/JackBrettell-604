using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; set; }
    public int CurrentMoney { get; private set; } = 0;

    public event System.Action<int> OnMoneyChanged; // Event to notify money changes

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMoney(100);
        }
    }
    

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney); // Trigger the event
    }

    public bool RemoveMoney(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            OnMoneyChanged?.Invoke(CurrentMoney); // Trigger the event
            return true;
        }
        return false;
    }
}
