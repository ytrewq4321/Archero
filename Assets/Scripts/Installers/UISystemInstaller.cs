using UnityEngine;
using Zenject;

public class UISystemInstaller : MonoInstaller
{
    [SerializeField] private CurrencyPanel moneyView;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<MoneyPanelAdapter>()
            .AsSingle()
            .WithArguments(moneyView)
            .NonLazy();
    }
}
