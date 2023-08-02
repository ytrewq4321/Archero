public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void LogicUpdate() { }

    public virtual void FixedUpdate() { }

    public virtual void Exit() { }
}