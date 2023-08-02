public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        //SetAnimationBool("Idle", true);
    }

    public override void LogicUpdate()
    {
        if(player.IsMove())
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else
        {
            player.FindNearestEnemy();
            if(player.Target!=null)
            {
                stateMachine.ChangeState(player.AttackState);
            }
        }
    }

    public override void Exit()
    {
        //SetAnimationBool("Idle", false);
    }
}
