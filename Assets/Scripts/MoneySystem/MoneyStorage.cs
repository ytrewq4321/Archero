using System;

[Serializable]
public class MoneyStorage
{
    public event Action<int> OnMoneyChanged;
    public int Money { get { return money; } }

    private int money;

    public void SetupMoney(int money)
    {
        this.money = money;
    }

    public void AddMoney(int range)
    {
        money += range;
        OnMoneyChanged.Invoke(money);
    }

    public void SpendMoney(int range)
    {
        money -= range;
        OnMoneyChanged.Invoke(money);
    }
}
