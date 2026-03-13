using System;
using DG.Tweening;
using UnityEngine;

public class NormalEnemy : EnemyBase
{
    public float HP = 100;

    protected override void Awake()
    {
        base.Awake();
        Initialize(_config.EnemyMaxHP);
    }

    public void Push(float force)
    {
        float targetPos = Rb.position.x + force;
        Rb.DOMoveX(targetPos, 0.3f).SetEase(Ease.OutQuad);
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
