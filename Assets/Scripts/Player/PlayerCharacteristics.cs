public class PlayerCharacteristics : IPlayerCharacteristics
{
    public float Damage => config.Damage;

    public float AttackRate => config.AttackRate;

    public float Speed => config.Speed;

    public float MaxHealth => config.MaxHealth;

    private Player—haracteristicsConfig config;

    public PlayerCharacteristics(Player—haracteristicsConfig config)
    {
        this.config = config;
    }
}
