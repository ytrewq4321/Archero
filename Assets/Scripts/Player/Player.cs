using UnityEngine;
using System;

public class Player : MonoBehaviour, IDamagable, IStartGameListener
{
    [SerializeField] private Player—haracteristicsConfig playerConfig;

    public PlayerStateMachine PlayerBehaivorSM { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public Transform Target { get; private set; }

    public event Action PlayerDied;  
    public Action ShootingStarted;

    private IPlayerCharacteristics playerCharacteristics;
    private PlayerCombat playerCombat;
    private PlayerMove playerController;
    private HealthSystem healthSystem;

    private void Awake()
    {
        InitializePlayer();
        enabled = false;
    }

    private void Update()
    {
        PlayerBehaivorSM.CurrentState.LogicUpdate();      
    }

    private void FixedUpdate()
    {
        PlayerBehaivorSM.CurrentState.FixedUpdate();
    }

    private void InitializePlayer()
    {
        IninializeStateMachine();

        playerCombat = GetComponent<PlayerCombat>();
        playerController = GetComponent<PlayerMove>();
        playerCharacteristics = new PlayerCharacteristics(playerConfig);

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(playerCharacteristics.MaxHealth);
        healthSystem.Died += OnPlayerDeath;
        ShootingStarted += Attack;
    }

    private void IninializeStateMachine()
    {
        PlayerBehaivorSM = new PlayerStateMachine();
        MoveState = new PlayerMoveState(this, PlayerBehaivorSM);
        IdleState = new PlayerIdleState(this, PlayerBehaivorSM);
        AttackState = new PlayerAttackState(this, PlayerBehaivorSM);

        PlayerBehaivorSM.Initialize(IdleState);
    }

    private void OnPlayerDeath()
    {
        PlayerDied.Invoke();
    }
  
    public void FindNearestEnemy()
    {
        Target = playerCombat.FindNearestEnemy(transform.position);      
    }

    public void LookAtTarget()
    {
        playerController.LookAtTarget(Target.position);
    }

    public void Attack()
    {
        playerCombat.Attack(Target.position, playerCharacteristics.Damage, playerCharacteristics.AttackRate);
    }

    public void Move()
    {
        playerController.Move(playerCharacteristics.Speed);
    }

    public void Look()
    {
        playerController.Look();
    }

    public bool IsMove()
    {
        return playerController.IsMove();
    }

    public void TakeDamage(float value)
    {
        healthSystem.TakeDamage(value);
    }

    public void OnStartGame()
    {
        enabled = true;
    }

    //public void SetAnimationBool(string anim, bool value)
    //{
    //    throw new System.NotImplementedException();
    //}
}
