using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격관련 기능
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private CombatActionEvents combatActionEvents;
    [SerializeField] private LayerMask enemyLayer;

    public float AttackRange = 1f;
    public float AttackDamage = 5f;
    public float AttackInterval = 0.3f;

    private readonly Collider2D[] _hitBuffer = new Collider2D[32];
    private readonly List<NormalEnemy> _normalBuffer = new();

    private Coroutine _attackRoutine;
    public void AttackStart()
    {
        if (_attackRoutine != null) return;
        _attackRoutine = StartCoroutine(AttackLoop());
    }

    public void AttackStop()
    {
        if (_attackRoutine == null) return;

        StopCoroutine(_attackRoutine);
        _attackRoutine = null;
    }

    private IEnumerator AttackLoop()
    {
        var interval = new WaitForSeconds(AttackInterval);
        while (true)
        {
            PerformAttack();
            yield return interval;
        }
    }

    private void PerformAttack()
    {
        _normalBuffer.Clear();

        int hitCount = Physics2D.OverlapCircleNonAlloc(
            transform.position, AttackRange, _hitBuffer, enemyLayer
        );
        for (int i = 0; i < hitCount; i++)
        {
            // EnemyBase 한 번만 가져온 후 타입 분기 → GetComponent 호출 최소화
            if (!_hitBuffer[i].TryGetComponent<EnemyBase>(out var enemy)) continue;

            switch (enemy)
            {
                case NormalEnemy normal:
                    _normalBuffer.Add(normal);
                    break;

                case EliteEnemy elite:
                    combatActionEvents.RequestEliteAttack(elite, AttackDamage);
                    break;
            }
        }

        if (_normalBuffer.Count > 0)
            combatActionEvents.RequestNormalAttack(_normalBuffer, AttackDamage);
    }
}
