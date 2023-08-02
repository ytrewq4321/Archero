using Zenject;
using System;

public class MoneyPanelAdapter : IInitializable, IDisposable
{
    private CurrencyPanel view;
    private MoneyStorage storage;

    public MoneyPanelAdapter(MoneyStorage storage, CurrencyPanel view)
    {
        this.view = view;
        this.storage = storage;
    }

    public void Initialize()
    {
        storage.OnMoneyChanged += OnMoneyChanged;
        view.SetupCurrency(storage.Money.ToString());
    }
    public void Dispose()
    {
        storage.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        view.UpdateCurrency(money.ToString());
    }
}