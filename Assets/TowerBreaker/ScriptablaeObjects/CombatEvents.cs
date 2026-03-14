using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEvents", menuName = "Scriptable Objects/DefenseEvents")]
public class CombatEvents : ScriptableObject
{
    public event Action<List<NormalEnemy>, float> OnNormalAttack;
    public event Action<EliteEnemy, float> OnEliteAttack;
    public event Action<float> OnNormalDefense;
    public event Action<EliteEnemy, float> OnEliteDefense;

    public void RequestNormalAttack(List<NormalEnemy> enemies, float force)
    {
        OnNormalAttack?.Invoke(enemies, force);
    }

    public void RequestEliteAttack(EliteEnemy enemy, float damage)
    {
        OnEliteAttack?.Invoke(enemy, damage);
    }

    public void RequestNormalDefense(float force)
    {
        OnNormalDefense?.Invoke(force);
    }

    public void RequestEliteDefense(EliteEnemy enemy, float force)
    {
        OnEliteDefense?.Invoke(enemy, force);
    }
}
