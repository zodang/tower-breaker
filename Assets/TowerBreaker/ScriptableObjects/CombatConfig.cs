using UnityEngine;

[CreateAssetMenu(fileName = "CombatConfig", menuName = "Scriptable Objects/CombatConfig")]
public class CombatConfig : ScriptableObject
{
    [Header("플레이어 위치")]
    public float PlayerOriginalPos = -1.75f;
    public float LeftWallX = -2.1f;

    [Header("플레이어 이동")]
    public float DashSpeed = 20f;
    public float DashDuration = 3f;
    public float DashCooldown = 0.5f;
    public float DashGap = 0.5f;

    [Header("플레이어 방어")]
    public float DefensePushRange = 3f;
    public float DefensePushForce = 10f;
    public float DefenseCooldown = 0.8f;
    public float DefenseGap = 0.5f;

    [Header("플레이어 공격")]
    public float AttackRange = 2.5f;
    public float AttackDamage = 10f;
    public float AttackInterval = 1f;

    [Header("적 이동")]
    public float EnemyMoveSpeed = 1.5f;
    public float EnemyMaxHP = 60f;

    [Header("생명")]
    public int MaxLives = 3;
}
