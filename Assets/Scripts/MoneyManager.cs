using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int CurrentMoney { get; private set; } = 0;

    public delegate void MoneyChanged(int newAmount);
    public event MoneyChanged OnMoneyChanged;

    public int giveMoneyAmount = 10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMoney(giveMoneyAmount);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
}

