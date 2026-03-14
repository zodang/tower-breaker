
using UnityEngine;
using UnityEngine.Rendering;

public class EliteEnemy : EnemyBase
{
    public EliteEnemyType EliteType;
    public float EliteHp = 10f;
    public float EliteSpeed = 4f;
    public float EliteWeight = 2f;

    protected override void Awake()
    {
        base.Awake();
        Initialize(EliteHp, EliteSpeed, EliteWeight);
    }

    private void OnEnable()
    {
        combatEvents.OnEliteAttack += HandleAttack;
        combatEvents.OnEliteDefense += HandleEliteDefense;
    }

    private void OnDisable()
    {
        combatEvents.OnEliteAttack -= HandleAttack;
        combatEvents.OnEliteDefense -= HandleEliteDefense;
    }

    private void HandleAttack(EliteEnemy enemy, float force)
    {
        if (enemy != this) return;

        TakeDamage(force);
    }

    private void HandleEliteDefense(EliteEnemy enemy, float force)
    {
        if (enemy != this) return;
        PushBack(force);
    }

    protected override void OnTakeDamage(float damage)
    {
        // 피격 효과 (추후 추가 가능)
    }

    protected override void OnDie()
    {
        // 죽을 때 이펙트
    }
}
