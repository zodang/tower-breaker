using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 방어 관련 기능
/// </summary>
public class PlayerDefense : MonoBehaviour
{
    [SerializeField] private CombatConfig config;
    [SerializeField] private CombatEvents combatEvents;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Rigidbody2D rigidBody;

    private bool _canDefense = true;

    private readonly HashSet<Collider2D> _facingEnemies = new();

    public void Defense()
    {
        if (!_canDefense) return;
        if (GetForwardGap() > config.DefenseGap) return;

        Debug.Log("방어");
        StartCoroutine(DefenseRoutine());

    }

    private IEnumerator DefenseRoutine()
    {
        _canDefense = false;

        combatEvents.RequestDefense(transform.position, config.DefensePushForce);
        ReturnOriginalPosition();
        yield return new WaitForSeconds(config.DefenseCooldown);

        _canDefense = true;
    }

    private Tween _sequence;
    private void ReturnOriginalPosition()
    {
        _sequence?.Kill();
        rigidBody.linearVelocity = Vector2.zero;

        _sequence = DOTween.To(
                () => rigidBody.position.x,
                x => rigidBody.MovePosition(new Vector2(x, rigidBody.position.y)),
                config.PlayerOriginalPos,
                0.15f
            )
            .SetEase(Ease.OutQuad);
    }

    private float GetForwardGap()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 20f, enemyLayer);
        if (hit.collider != null) return hit.distance;
        return 20f;
    }
}
