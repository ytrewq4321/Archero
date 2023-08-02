public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void LogicUpdate()
    {
        if(!enemy.IsPlayerInSight())
        {
            stateMachine.ChangeState(enemy.IdleState);
        }

        enemy.Look();
        enemy.Attack();
    }
}
