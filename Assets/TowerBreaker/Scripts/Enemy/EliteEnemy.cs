using UnityEngine;

public class EliteEnemy : EnemyBase
{
    [SerializeField] private EnemyData enemyData;

    protected override void Awake()
    {
        base.Awake();
        Initialize(enemyData.Hp, enemyData.Speed, enemyData.Weight);
    }

    private void OnEnable()
    {
        combatEvents.RegisterElite(this, HandleAttack, HandleDefense);
    }

    private void OnDisable()
    {
        combatEvents.UnregisterElite(this);
    }

    private void HandleAttack(float damage)
    {
        TakeDamage(damage); // self-check 불필요
    }

    private void HandleDefense(float force)
    {
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
