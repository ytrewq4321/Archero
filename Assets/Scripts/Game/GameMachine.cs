using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAY = 0,
    PAUSE = 1,
}

public class GameMachine : IGameMachine
{
    private GameState gameState = GameState.PAUSE;

    public GameState GameState
    {
        get { return gameState; }
    }

    private readonly  List<object> listeners;

    public GameMachine()
    {
        listeners = new List<object>();
    }

    public void StartGame()
    {
        gameState = GameState.PLAY;

        foreach (var listener in listeners)
        {
            if (listener is IStartGameListener startGameListener)
            {
                startGameListener.OnStartGame();
            }
        }
    }

    public void AddListener(object listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(object listener)
    {
        listeners.Remove(listener);
    }
}
