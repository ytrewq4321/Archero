using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private HealthPanel healthPanel;
    private HealthStorage healthStorage;
    public event Action Died;

    public void Initialize(float maxHealth)
    {
        healthStorage = new HealthStorage(maxHealth);
        new HealthAdapter(healthStorage, healthPanel);
    }

    public void TakeDamage(float value)
    {
        healthStorage.ReduceHealth(value);
        if(healthStorage.Health<=0)
        {
            Died.Invoke();
        }
    }
}
