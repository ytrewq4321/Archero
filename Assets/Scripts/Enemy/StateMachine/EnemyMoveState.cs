public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        //start animition
        enemy.StartAgent();
        enemy.Move();
    }

    public override void LogicUpdate()
    {
        enemy.Look();

        if (enemy.IsPlayerTouched())
        {
            stateMachine.ChangeState(enemy.AttackState);
        }

        if (enemy.IsPathComplete())
        {
            stateMachine.ChangeState(enemy.IdleState);
        }    
    }

    public override void Exit()
    {
        enemy.StopAgent();
        //stop animition    
    }
}
