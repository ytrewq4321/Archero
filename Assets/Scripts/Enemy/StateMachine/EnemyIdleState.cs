public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        //animation start
    }

    public override void LogicUpdate()
    {
        enemy.IdleTimer();
    }

    public override void Exit()
    {
        //animation stop
    }
}
