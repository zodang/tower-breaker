using UnityEngine;

public class BlockZone : MonoBehaviour
{
    [SerializeField] private StageProgressEvents stageProgressEvents;
    [SerializeField] private Collider2D collider;

    private void OnEnable()
    {
        stageProgressEvents.OnFloorStarted += EnableCollision;
        stageProgressEvents.OnFloorCleared += DisableCollision;
    }

    private void OnDisable()
    {
        stageProgressEvents.OnFloorStarted -= EnableCollision;
        stageProgressEvents.OnFloorCleared -= DisableCollision;
    }

    private void EnableCollision() => collider.enabled = true;
    private void DisableCollision() => collider.enabled = false;
}
