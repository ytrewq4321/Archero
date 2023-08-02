using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "PlayerСharacteristicsConfig/Config", order = 1)]
public class PlayerСharacteristicsConfig : ScriptableObject, IPlayerCharacteristics
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float attackRate;
    [SerializeField] private float damage;

    public float Damage
    {
        get { return damage; }
    }

    public float AttackRate
    {
        get { return attackRate; }
    }

    public float Speed
    {
        get { return speed; }
    }

    public float MaxHealth
    {
        get { return health; }
    } 
}