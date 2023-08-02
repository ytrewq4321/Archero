using UnityEngine;

public interface IGameMachine
{
    GameState GameState { get; }

    void StartGame();

    public void AddListener(object listener);

    public void RemoveListener(object listener);
}