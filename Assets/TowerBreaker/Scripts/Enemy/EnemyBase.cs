using System;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected CombatActionEvents combatActionEvents;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public float MaxHp = 10f;
    public float CurrentHp;
    public float CurrentSpeed = 1f;
    public float CurrentWeight = 1f;

    public Rigidbody2D RigidBody { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsMoving = false;
    public bool IsPushing = false;

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

    protected virtual void FixedUpdate()
    {
        if (!IsMoving) return;
        if (IsPushing) return;
        RigidBody.MovePosition(RigidBody.position + Vector2.left * (Time.fixedDeltaTime * CurrentSpeed));
    }

    public virtual void PushBack(float force)
    {
        if (!IsMoving) return;
        IsPushing = true;

        // 기본 밀림
        float pushForce = force / CurrentWeight;
        float targetPos = RigidBody.position.x + force + pushForce;

        RigidBody.DOKill();
        RigidBody.DOMoveX(targetPos, 0.15f).SetEase(Ease.OutQuad).OnComplete(()=>IsPushing = false);
    }

    public virtual void TakeDamage(float damage)
    {
        if (!IsMoving) return;

        // 기본 피격
        CurrentHp -= damage;
        OnTakeDamage(damage);

        CameraEffect.Instance.Shake();

        if (CurrentHp <= 0)
        {
            Die();
            return;
        }

        HitEffect();
    }

    private void HitEffect()
    {
        spriteRenderer.DOKill();
        spriteRenderer.color = Color.white;

        spriteRenderer.DOColor(Color.red, 0.05f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
                spriteRenderer.DOColor(Color.white, 0.15f)
                    .SetEase(Ease.InQuad));
    }

    public virtual void Die()
    {
        OnDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected virtual void OnTakeDamage(float damage) { }
    protected virtual void OnDie() { }
}
