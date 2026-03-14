using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격관련 기능
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private CombatConfig config;
    [SerializeField] private CombatEvents combatEvents;
    [SerializeField] private LayerMask enemyLayer;

    private Coroutine _attackRoutine;
    public void AttackStart()
    {
        Debug.Log("공격 시작");
        if (_attackRoutine != null) return;
        _attackRoutine = StartCoroutine(AttackLoop());
    }

    public void AttackStop()
    {
        StopCoroutine(_attackRoutine);
        _attackRoutine = null;
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            PerformAttack();
            yield return new WaitForSeconds(config.AttackInterval);
        }
    }

    private void PerformAttack()
    {
        var normal = new List<NormalEnemy>();

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, config.AttackRange, enemyLayer);

        foreach (var col in hits)
        {
            if (col.TryGetComponent<NormalEnemy>(out var n))
            {
                normal.Add(n);
            }

            if (col.TryGetComponent<EliteEnemy>(out var elite))
            {
                combatEvents.RequestEliteAttack(elite, config.AttackDamage);
            }
        }

        combatEvents.RequestNormalAttack(normal, config.AttackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.AttackRange);
    }
}
