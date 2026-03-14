public class NormalEnemy : EnemyBase
{
    public float NormalHp = 10f;
    public float NormalSpeed = 4f;
    public float NormalWeight = 1f;

    protected override void Awake()
    {
        base.Awake();
        Initialize(NormalHp, NormalSpeed, NormalWeight);
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
