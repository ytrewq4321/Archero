using UnityEngine;
using UnityEngine.AI;
using Zenject;


public class Enemy : MonoBehaviour, IStartGameListener
{
    public EnemyAttackState AttackState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float rotationSmoothTime;
    [SerializeField] private LayerMask projectileMask;

    private NavMeshAgent agent;
    private Transform playerTranfsorm;
    private ProjectileSpawner projectileSpawner;
    private HealthSystem healthSystem;
    private EnemyHandler enemyHandler;

    private float timeSinceLastDamage = 0f;
    private float idleTimer;
    private float time;
    private bool isPlayerTouched;
    private Vector3 direction;
    private float rotationVelocity;
    private IDamagable damagable;

    [Inject]
    public void Constructor(Player player, ProjectileSpawner projectileSpawner, EnemyHandler enemyHandler)
    {
        this.playerTranfsorm = player.transform;
        this.projectileSpawner = projectileSpawner;
        this.enemyHandler = enemyHandler;
    }

    private void Awake()
    {
        Initialize();
        enabled = false;
    }

    private void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(enemyConfig.Health);
        healthSystem.Died += OnEnemyDeath;

        StateMachine = new EnemyStateMachine();
        AttackState = new EnemyAttackState(this, StateMachine);
        MoveState = new EnemyMoveState(this, StateMachine);
        IdleState = new EnemyIdleState(this, StateMachine);
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public void OnEnemyDeath()
    {
        enemyHandler.RemoveEnemy(this);
        Destroy(gameObject);
    }


    public void IdleTimer()
    {
        idleTimer += Time.deltaTime;
        if(idleTimer>=enemyConfig.IdleTime)
        {
            if(IsPlayerInSight())
            {
                StateMachine.ChangeState(AttackState);
            }
            else
            {
                StateMachine.ChangeState(MoveState);
            }
            idleTimer = 0;
        }
    }


    public void Attack()
    {
        SetShootDirection();

        time += Time.deltaTime;
        if(time>=1f/enemyConfig.AttackRate)
        {
            projectileSpawner.Spawn(projectileSpawnPoint.position, direction, enemyConfig.Damage,ProjectileOwner.ENEMY);
            time = 0;
        }
    }

    public void SetLookDirection()
    {
        direction = agent.desiredVelocity;
    }

    public void SetShootDirection()
    {
        direction = (playerTranfsorm.position - projectileSpawnPoint.position).normalized;
    }

    public void Move()
    {
        Vector3 point;
        if(RandomPoint(transform.position, enemyConfig.TravelRange, out point))
        {
            agent.SetDestination(point);
            SetLookDirection();
        }
    }

    public void StartAgent()
    {
        agent.isStopped = false;
    }
    public void StopAgent()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    public bool IsPathComplete()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void Look()
    {
        if (direction != Vector3.zero)
        {
            var targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public void TakeDamage(float value)
    {
        healthSystem.TakeDamage(value);
    }

    public bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = playerTranfsorm.position - transform.position;
        Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit1, Mathf.Infinity, ~projectileMask);
        return hit1.transform == playerTranfsorm;
    }

    public bool IsPlayerTouched()
    {
        return isPlayerTouched;
    }

    private void OnCollisionEnter(Collision collision)
    {
        damagable = collision.collider.GetComponent<IDamagable>();
        isPlayerTouched = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(damagable!=null)
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= enemyConfig.TouchDamageInterval)
            {
                damagable.TakeDamage(enemyConfig.TouchDamage);
                timeSinceLastDamage = 0f;
            }
        }
       
    }

    private void OnCollisionExit(Collision collision)
    {
        timeSinceLastDamage = 0f;
        isPlayerTouched = false;
        damagable = null;
    }
    public void OnStartGame()
    {
        enabled = true;
    }

    private void OnDestroy()
    {
        healthSystem.Died -= OnEnemyDeath;
    }

    //public void SetAnimationBool(string anim, bool value)
    //{
    //    throw new System.NotImplementedException();
    //}
}
