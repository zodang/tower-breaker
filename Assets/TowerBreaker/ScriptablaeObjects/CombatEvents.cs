using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEvents", menuName = "Scriptable Objects/DefenseEvents")]
public class CombatEvents : ScriptableObject
{
    // 일반 적 공격 — NormalSquad 전체가 밀려남
    public event Action<List<NormalEnemy>, float> OnNormalAttack;

    // 특수 적 공격 — 해당 EliteEnemy만 피격
    public event Action<EliteEnemy, float> OnEliteAttack;

    // 방어 — 가장 가까운 NormalSquad가 밀려남
    public event Action<Vector2, float> OnDefense;

    public void RequestNormalAttack(List<NormalEnemy> enemies, float force)
    {
        OnNormalAttack?.Invoke(enemies, force);
    }

    public void RequestEliteAttack(EliteEnemy target, float damage)
    {
        OnEliteAttack?.Invoke(target, damage);
    }

    public void RequestDefense(Vector2 playerPos, float force)
    {
        OnDefense?.Invoke(playerPos, force);
    }
}
