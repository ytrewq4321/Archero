public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void LogicUpdate()
    {
        if(player.Target!=null)
        {
            player.LookAtTarget();
            player.Attack();
        }
        else
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.IsMove())
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }
}
