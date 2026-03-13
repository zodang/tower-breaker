using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected CombatConfig _config;

    public Rigidbody2D Rb { get; private set; }
    public bool IsDead { get; private set; }

    protected float _maxHP;
    protected float _currentHP;

    public Action<EnemyBase> OnDied;

    protected virtual void Awake()
    {
        Rb = GetComponentInChildren<Rigidbody2D>();
    }

    protected virtual void Initialize(float hp)
    {
        _maxHP = hp;
        _currentHP = _maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHP -= damage;

        OnTakeDamage(damage);

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected virtual void OnTakeDamage(float damage) { }
    protected virtual void OnDie() { }
}
