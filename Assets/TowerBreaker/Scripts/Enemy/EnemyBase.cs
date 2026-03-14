using System;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected CombatActionEvents combatActionEvents;

    public float MaxHp = 10f;
    public float CurrentHp;
    public float CurrentSpeed = 1f;
    public float CurrentWeight = 1f;

    public Rigidbody2D RigidBody { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsMoving { get; private set; } = false;

    public void StartMoving() => IsMoving = true;
    public void StopMoving()  => IsMoving = false;

    public Action<EnemyBase> OnDied;

    protected virtual void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    protected void Initialize(float hp, float speed, float weight)
    {
        MaxHp = hp;
        CurrentHp = MaxHp;
        CurrentSpeed = speed;
        CurrentWeight = weight;
    }

    protected virtual void Update()
    {
        // 기본 이동
        if (!IsMoving) return;
        RigidBody.MovePosition(transform.position + Vector3.left * (Time.deltaTime * CurrentSpeed));
    }

    public virtual void PushBack(float force)
    {
        if (!IsMoving) return;

        // 기본 밀림
        float pushForce = force / CurrentWeight;
        float targetPos = RigidBody.position.x + force + pushForce;

        RigidBody.DOKill();
        RigidBody.DOMoveX(targetPos, 0.15f).SetEase(Ease.OutQuad);
    }

    public virtual void TakeDamage(float damage)
    {
        if (!IsMoving) return;

        // 기본 피격
        CurrentHp -= damage;
        OnTakeDamage(damage);

        if (CurrentHp <= 0) Die();
    }

    public virtual void Die()
    {
        OnDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected virtual void OnTakeDamage(float damage) { }
    protected virtual void OnDie() { }
}
