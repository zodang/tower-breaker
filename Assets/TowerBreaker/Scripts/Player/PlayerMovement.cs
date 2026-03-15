using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 이동 관련 기능
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D RigidBody => rigidBody;

    [SerializeField] private StageProgressEvents stageEvents;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D collider;
    [SerializeField] private LayerMask blockLayer;

    public float DashCooldown = 0.5f;

    private bool _isMoving = false;
    private bool _canMove = true;

    private Tween _sequence;

    public void SetControllable(bool value)
    {
        _canMove = value;
    }

    public void Move()
    {
        if (!_canMove || _isMoving) return;

        float gap = GetForwardGap();
        if (gap < 0.15f) return;

        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        _canMove = false;
        _isMoving = true;

        ReturnOriginalPosition();
        yield return new WaitForSeconds(DashCooldown);

        _canMove = true;
        _isMoving = false;
    }

    public void ReturnOriginalPosition()
    {
        _sequence?.Kill();
        rigidBody.linearVelocity = Vector2.zero;

        float endValue = rigidBody.position.x + GetForwardGap() - 0.15f;

        _sequence = DOTween.To(
                () => rigidBody.position.x,
                x => rigidBody.MovePosition(new Vector2(x, rigidBody.position.y)),
                endValue,
                0.15f
            )
            .SetEase(Ease.OutQuad);
    }

    public void StepForward(float distance)
    {
        _sequence?.Kill();
        rigidBody.linearVelocity = Vector2.zero;

        float endValue = rigidBody.position.x + distance;

        _sequence = DOTween.To(
                () => rigidBody.position.x,
                x => rigidBody.MovePosition(new Vector2(x, rigidBody.position.y)),
                endValue,
                0.05f
            )
            .SetEase(Ease.OutQuad);
    }

    private float GetForwardGap()
    {
        Vector2 rayOrigin = new Vector2(collider.bounds.max.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, 20f, blockLayer);

        return hit.collider != null ? hit.distance : 20f;
    }
}
