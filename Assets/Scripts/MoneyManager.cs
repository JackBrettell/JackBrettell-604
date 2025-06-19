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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMoney(100);
        }

#endif
    }
    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney); 
    }

    public bool RemoveMoney(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            OnMoneyChanged?.Invoke(CurrentMoney); 
            return true;
        }
        return false;
    }

    public void SetMoney(int amount)
    {
        CurrentMoney = amount;
    }

}
