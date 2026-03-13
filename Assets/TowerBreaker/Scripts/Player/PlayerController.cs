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

    private void OnEnable()
    {
        playerInputEvents.OnMoveRequested += HandleMove;
        playerInputEvents.OnDefenseRequested += HandleDefense;
        playerInputEvents.OnAttackRequested += HandleAttack;
    }

    private void OnDisable()
    {
        playerInputEvents.OnMoveRequested -= HandleMove;
        playerInputEvents.OnDefenseRequested -= HandleDefense;
        playerInputEvents.OnAttackRequested -= HandleAttack;
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

    private  void HandleAttack()
    {
        _playerAttack.Attack();
    }
}
