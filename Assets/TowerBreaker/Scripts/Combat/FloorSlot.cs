using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorSlot : MonoBehaviour
{
    public event Action OnFloorCleared;

    [SerializeField] public Transform spawnPoint;
    [SerializeField] private NormalCluster normalCluster;

    private readonly List<EnemyBase> _aliveEnemies = new();
    private readonly List<EnemyBase> _deadEnemies = new();

    public void RegisterEnemy(EnemyBase enemy)
    {
        _aliveEnemies.Add(enemy);

        if (enemy is NormalEnemy normalEnemy) normalCluster.Register(normalEnemy);
        enemy.OnDied += HandleEnemyDied;
    }

    private void HandleEnemyDied(EnemyBase enemy)
    {
        enemy.OnDied -= HandleEnemyDied;

        _aliveEnemies.Remove(enemy);
        _deadEnemies.Add(enemy);

        if (_aliveEnemies.Count == 0) OnFloorCleared?.Invoke();
    }

    public bool IsCleared()
    {
        return _aliveEnemies.Count == 0;
    }

    public void ClearEnemies()
    {
        foreach (var enemy in _aliveEnemies)
            Destroy(enemy.gameObject);

        _aliveEnemies.Clear();
        _deadEnemies.Clear();
    }
}
