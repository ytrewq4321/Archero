using System;
using Zenject;

public class HealthAdapter : IInitializable, IDisposable
{
    private HealthPanel view;
    private HealthStorage storage;

    public HealthAdapter(HealthStorage storage, HealthPanel view)
    {
        this.view = view;
        this.storage = storage;
        Initialize();
    }

    public void Initialize()
    {
        storage.OnHealthChanged += OnHealthChanged;
        view.SetupHealth(storage.Health);
    }

    public void Dispose()
    {
        storage.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        view.UpdateHealth(health);
    }
}
