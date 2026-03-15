using System;
using UnityEngine;

/// <summary>
/// 플레이어 기능 조합
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputEvents playerInputEvents;

    private PlayerMovement _playerMovement;
    private PlayerDefense _playerDefense;
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        playerInputEvents.OnMoveRequested += HandleMove;
        playerInputEvents.OnDefenseRequested += HandleDefense;
        playerInputEvents.OnAttackStartRequested += HandleAttackStart;
        playerInputEvents.OnAttackStopRequested += HandleAttackStop;
    }

    private void OnDisable()
    {
        playerInputEvents.OnMoveRequested -= HandleMove;
        playerInputEvents.OnDefenseRequested -= HandleDefense;
        playerInputEvents.OnAttackStartRequested -= HandleAttackStart;
        playerInputEvents.OnAttackStopRequested -= HandleAttackStop;
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerDefense = GetComponent<PlayerDefense>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void HandleMove()
    {
        _playerMovement.Move();
    }

    private  void HandleDefense()
    {
        _playerDefense.Defense();
    }

    private  void HandleAttackStart()
    {
        _playerAttack.AttackStart();
    }

    private  void HandleAttackStop()
    {
        _playerAttack.AttackStop();
    }
}
