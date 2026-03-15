using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalCluster : MonoBehaviour
{
    [SerializeField] private CombatActionEvents combatActionEvents;
    private readonly List<NormalEnemy> _units = new();

    private float _speedMultiple = 1f;
    private float _hpMultiple = 1;
    private bool _isPushing = false;

    private void OnEnable()
    {
        combatActionEvents.OnNormalAttack += HandleNormalAttack;
        combatActionEvents.OnNormalDefense += HandleNormalDefense;
    }

    private void OnDisable()
    {
        combatActionEvents.OnNormalAttack -= HandleNormalAttack;
        combatActionEvents.OnNormalDefense -= HandleNormalDefense;
    }

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

    private void HandleNormalAttack(List<NormalEnemy> enemies, float force)
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            enemy.TakeDamage(force);
        }
    }

    private void HandleNormalDefense(float force)
    {
        PushEnemies(force);
    }

    private void PushEnemies(float force)
    {
        if (_isPushing) return;

        foreach (var enemy in _units)
        {
            if (enemy == null) continue;

            enemy.PushBack(force);
        }
    }
}
