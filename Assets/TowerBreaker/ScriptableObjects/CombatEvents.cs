using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEvents", menuName = "Scriptable Objects/DefenseEvents")]
public class CombatEvents : ScriptableObject
{
    public event Action<List<NormalEnemy>, float> OnNormalAttack;
    public event Action<float> OnNormalDefense;

    private readonly Dictionary<EliteEnemy, Action<float>> _eliteAttackHandlers = new();
    private readonly Dictionary<EliteEnemy, Action<float>> _eliteDefenseHandlers = new();

    public void RequestNormalAttack(List<NormalEnemy> enemies, float force)
    {
        OnNormalAttack?.Invoke(enemies, force);
    }

    public void RequestNormalDefense(float force)
    {
        OnNormalDefense?.Invoke(force);
    }

    public void RequestEliteAttack(EliteEnemy enemy, float damage)
    {
        if (_eliteAttackHandlers.TryGetValue(enemy, out var handler))
            handler?.Invoke(damage);
    }

    public void RequestEliteDefense(EliteEnemy enemy, float force)
    {
        if (_eliteDefenseHandlers.TryGetValue(enemy, out var handler))
            handler?.Invoke(force);
    }

    public void RegisterElite(EliteEnemy enemy, Action<float> onAttack, Action<float> onDefense)
    {
        _eliteAttackHandlers[enemy] = onAttack;
        _eliteDefenseHandlers[enemy] = onDefense;
    }

    public void UnregisterElite(EliteEnemy enemy)
    {
        _eliteAttackHandlers.Remove(enemy);
        _eliteDefenseHandlers.Remove(enemy);
    }

    private void OnEnable()
    {
        _eliteAttackHandlers.Clear();
        _eliteDefenseHandlers.Clear();
    }
}
