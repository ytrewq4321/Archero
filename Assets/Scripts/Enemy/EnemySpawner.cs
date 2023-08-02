using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemiesCount;
    [SerializeField] private EnemyType enemyType;

    [Header("SpawnArea")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private EnemyFactory enemyFactory;
    private EnemyHandler enemyHandler;

    [Inject]
    public void Constructor(EnemyHandler enemyHandler, EnemyFactory enemyFactory)
    {
        this.enemyHandler = enemyHandler;
        this.enemyFactory = enemyFactory;
    }

    private void Start()
    {
        CreateEnemies();
    }

    public bool RandomPoint(out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = new Vector3(Random.Range(startPoint.position.x, endPoint.position.x ), startPoint.position.y,
                Random.Range(startPoint.position.z , endPoint.position.z ));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position+transform.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void CreateEnemies()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            if(RandomPoint(out Vector3 position))
            {
                var enemy = enemyFactory.Create(enemyType, position).GetComponent<Enemy>();
                enemyHandler.AddEnemy(enemy);
            } 
        }
    }
}