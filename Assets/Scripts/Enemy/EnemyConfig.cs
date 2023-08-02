
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Enemy/Config", order = 1)]
public class EnemyConfig : ScriptableObject
{
    public float Speed;
    public float Health;
    public float Damage;
    public float IdleTime;
    public float AttackRate;
    public float TouchDamage;
    public float TouchDamageInterval;
    public float TravelRange;
}
