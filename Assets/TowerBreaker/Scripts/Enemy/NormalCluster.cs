using System.Collections.Generic;
using UnityEngine;

public class NormalCluster : MonoBehaviour
{
    [SerializeField] private CombatEvents combatEvents;

    public List<NormalEnemy> liveEnemies = new();
    public List<NormalEnemy> deadEnemies = new();

    private float _speedMultiple = 1f;
    private float _hpMultiple = 1;
    private bool _isPushing = false;

    private void OnEnable()
    {
        combatEvents.OnNormalAttack += HandleNormalAttack;
        combatEvents.OnNormalDefense += HandleNormalDefense;
    }

    private void OnDisable()
    {
        combatEvents.OnNormalAttack -= HandleNormalAttack;
        combatEvents.OnNormalDefense -= HandleNormalDefense;
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

    #region Manage Unit

    public void Register(NormalEnemy unit)
    {
        if (liveEnemies.Contains(unit)) return;
        unit.OnDied += HandleUnitDied;
        liveEnemies.Add(unit);
    }

    public void Unregister(NormalEnemy unit)
    {
        unit.OnDied -= HandleUnitDied;
        liveEnemies.Remove(unit);
    }

    private void HandleUnitDied(EnemyBase unit)
    {
        if (unit is NormalEnemy normalUnit) Unregister(normalUnit);
    }

    #endregion



    private void PushEnemies(float force)
    {
        if (_isPushing) return;

        foreach (var enemy in liveEnemies)
        {
            if (enemy == null) continue;

            enemy.PushBack(force);
        }
    }
}
