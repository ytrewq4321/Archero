using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoneySystemInstaller : MonoInstaller
{
    [SerializeField] private MoneyStorage moneyStorage;

    public override void InstallBindings()
    {
        Container
            .Bind<MoneyStorage>()
            .AsSingle()
            .OnInstantiated<MoneyStorage>((_, it) => moneyStorage = it)
            .NonLazy();
    }
}
