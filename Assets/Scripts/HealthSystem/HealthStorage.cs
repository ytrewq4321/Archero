using System;

public class HealthStorage
{
    public event Action<float> OnHealthChanged;

    public float Health { get { return health; } }

    private float health;

    public HealthStorage(float health)
    {
        this.health = health;
    }

    public void SetupHealth(float health)
    {
        this.health = health;
    }

    public void AddHealth(float range)
    {
        health += range;
        OnHealthChanged.Invoke(health);
    }

    public void ReduceHealth(float range)
    {
        health -= range;
        OnHealthChanged.Invoke(health);
    }
}
