using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHandler
{
    public List<Enemy> Enemies { get; private set; }
    public event Action AllEnemiesDied;
    public event Action EnemyDied;

    private readonly GameMachine gameMachine;

    public EnemyHandler(GameMachine gameMachine)
    {
        Enemies = new List<Enemy>();
        this.gameMachine = gameMachine;

    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        gameMachine.AddListener(enemy);
       
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        gameMachine.RemoveListener(enemy);
        EnemyDied.Invoke();
        if (Enemies.Count==0)
        {
            AllEnemiesDied.Invoke();
        }
    }
}
