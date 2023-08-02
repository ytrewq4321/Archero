using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Gate gate;
    private EnemyHandler enemyHandler;
    private MoneyStorage moneyStorage;
    private Player player;

    [Inject]
    public void Constructor(Player player, EnemyHandler enemyHandler, MoneyStorage moneyStorage)
    {
        this.player = player;
        this.enemyHandler = enemyHandler;
        this.moneyStorage = moneyStorage;
    }

    public void Start()
    {
        player.PlayerDied += DefeatGame;
        enemyHandler.AllEnemiesDied += OpenGate;
        enemyHandler.EnemyDied += AddMoney;
        gate.PassedGate += WinGame;
    }

    public void AddMoney()
    {
        moneyStorage.AddMoney(10);
    }
    
    public void OpenGate()
    {
        gate.OpenGate();
    }

    public void WinGame()
    {
        Debug.Log("WIN");
    }

    public void DefeatGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OnDestroy()
    {
        player.PlayerDied -= DefeatGame;
        enemyHandler.AllEnemiesDied -= OpenGate;
        enemyHandler.EnemyDied -= AddMoney;
        gate.PassedGate -= WinGame;
    }
}
