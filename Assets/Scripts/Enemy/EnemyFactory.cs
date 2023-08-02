using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private const string FlyingEnemyPrefabPath = "EnemyFlying";
    private const string WalkingEnemyPrefabPath = "EnemyWalking";

    private GameObject flyingEnemyPrefab;
    private GameObject walkingEnemyPrefab;

    private readonly DiContainer diContainer;

    public EnemyFactory(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }

    public GameObject Create(EnemyType type, Vector3 at)
    {
        switch (type)
        {
            case EnemyType.Flying:
                return diContainer.InstantiatePrefab(flyingEnemyPrefab, at, Quaternion.identity, null);
            case EnemyType.Walking:
                return diContainer.InstantiatePrefab(walkingEnemyPrefab, at, Quaternion.identity, null);
        }
        return null;
    }

    public void Load()
    {
        flyingEnemyPrefab = Resources.Load(FlyingEnemyPrefabPath) as GameObject;
        walkingEnemyPrefab = Resources.Load(WalkingEnemyPrefabPath) as GameObject;
    }
}
