using UnityEngine;

public interface IEnemyFactory
{
    GameObject Create(EnemyType type, Vector3 at);
    void Load();
}

public enum EnemyType
{
    Walking,
    Flying
}

