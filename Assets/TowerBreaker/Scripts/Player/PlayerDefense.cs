using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 방어 관련 기능
/// </summary>
public class PlayerDefense : MonoBehaviour
{
    [SerializeField] private CombatActionEvents combatActionEvents;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Rigidbody2D rigidBody;

    public float DefensePushForce = 0.5f;
    public float DefenseCooldown = 0.8f;
    public float DefenseGap = 0.5f;

    public float PlayerOriginalPos = -1.75f;
    private bool _canDefense = true;

    public void Defense()
    {
        if (!_canDefense) return;
        if (GetForwardGap() > DefenseGap) return;

        StartCoroutine(DefenseRoutine());
    }

    private IEnumerator DefenseRoutine()
    {
        _canDefense = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);

        foreach (var col in hits)
        {
            if (col.TryGetComponent<EliteEnemy>(out var elite))
            {
                combatActionEvents.RequestEliteDefense(elite, DefensePushForce);
            }
        }

        combatActionEvents.RequestNormalDefense(DefensePushForce);
        ReturnOriginalPosition();
        yield return new WaitForSeconds(DefenseCooldown);

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
                PlayerOriginalPos,
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
