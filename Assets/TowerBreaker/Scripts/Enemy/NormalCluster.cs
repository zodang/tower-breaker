using System.Collections.Generic;
using UnityEngine;

public class NormalCluster : MonoBehaviour
{
    public int Count => _units.Count;
    public bool IsEmpty => _units.Count == 0;

    [SerializeField] private CombatConfig config;
    [SerializeField] private CombatEvents combatEvents;

    private readonly List<NormalEnemy> _units = new();

    private float _speedMultiple = 1f;
    private float _hpMultiple = 1;
    private bool _isPushing = false;

    private void OnEnable()
    {
        combatEvents.OnNormalAttack += HandleNormalAttack;
        combatEvents.OnDefense += HandleDefense;
    }

    private void OnDisable()
    {
        combatEvents.OnNormalAttack -= HandleNormalAttack;
        combatEvents.OnDefense -= HandleDefense;
    }

    private void HandleNormalAttack(List<NormalEnemy> enemies, float force)
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            enemy.TakeDamage(force);
        }
    }

    private void HandleDefense(Vector2 playerPos, float force)
    {
        if (IsEmpty) return;
        // 현재 단계 군집인지 확인 필요

        PushEnemies(force);
    }

    #region Manage Unit

    public void Register(NormalEnemy unit)
    {
        if (_units.Contains(unit)) return;
        unit.OnDied += HandleUnitDied;
        _units.Add(unit);
    }

    public void Unregister(NormalEnemy unit)
    {
        unit.OnDied -= HandleUnitDied;
        _units.Remove(unit);
    }

    private void HandleUnitDied(EnemyBase unit)
    {
        if (unit is NormalEnemy normalUnit) Unregister(normalUnit);
    }

    #endregion



    private void PushEnemies(float force)
    {
        if (_isPushing) return;

        foreach (var enemy in _units)
        {
            if (enemy == null) continue;

            enemy.Push(force);
        }
    }
}
