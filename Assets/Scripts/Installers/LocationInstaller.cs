using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller, IInitializable
{
    public Transform StartPoint;
    public GameObject PlayerPrefab;
    public Player—haracteristicsConfig playerConfig;
    public GameObject ProjectilePrefab;

    public override void InstallBindings()
    {
        BindLocationInstaller();
        BindInputManager();
        BindProjectilePool();
        BindGameMachine();
        BindEnemyHandler();
        BindHero();
        BindEnemyFactory();
    }

    private void BindLocationInstaller()
    {
        Container
            .BindInterfacesTo<LocationInstaller>()
            .FromInstance(this);
    }

    private void BindInputManager()
    {
        Container
            .Bind<InputManager>()
            .AsSingle()
            .NonLazy();
    }

    private void BindHero()
    {
        Player player = Container.InstantiatePrefabForComponent<Player>(PlayerPrefab, StartPoint);

        Container.BindInterfacesAndSelfTo<Player>()
          .FromInstance(player)
          .AsSingle();      
    }

    private void BindEnemyHandler()
    {
        Container
            .Bind<EnemyHandler>()
            .AsSingle()
            .NonLazy();
    }
    private void BindEnemyFactory()
    {
        Container
            .BindInterfacesAndSelfTo<EnemyFactory>()
            .AsSingle()
            .NonLazy();
    }

    private void BindProjectilePool()
    {
        Container
            .Bind<ProjectileSpawner>()
            .AsSingle()
            .NonLazy();

        Container
          .BindMemoryPool<Projectile, Projectile.Pool>()
          .WithInitialSize(40)
          .FromComponentInNewPrefab(ProjectilePrefab)
          .UnderTransformGroup("Projectile");
    }

    private void BindGameMachine()
    {
        Container
            .BindInterfacesAndSelfTo<GameMachine>()
            .AsSingle()
            .NonLazy();
    }

    public void Initialize()
    {
        var enemyFactory = Container.Resolve<IEnemyFactory>();
        enemyFactory.Load();

        var player = Container.Resolve<Player>();
        var gameMachine = Container.Resolve<IGameMachine>();
        gameMachine.AddListener(player);
    }
}