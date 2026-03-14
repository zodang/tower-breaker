using UnityEngine;

public class NormalEnemy : EnemyBase
{
    [SerializeField] private EnemyData enemyData;

    protected override void Awake()
    {
        base.Awake();
        Initialize(enemyData.Hp, enemyData.Speed, enemyData.Weight);
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
