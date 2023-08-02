public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        //anim
    }

    public override void LogicUpdate()
    {
        if (!player.IsMove())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        player.Move();
        player.Look();
    }

    public override void Exit()
    {
        //anim
    }
}
