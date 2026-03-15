using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private StageProgressEvents stageProgressEvents;

    public int MaxLives = 3;
    public int CurrentLives { get; private set; }

    private void Awake()
    {
        CurrentLives = MaxLives;
    }

    private void OnEnable()
    {
        stageProgressEvents.OnPlayerDamaged += TakeDamage;
    }

    private void OnDisable()
    {
        stageProgressEvents.OnPlayerDamaged -= TakeDamage;
    }

    private void TakeDamage()
    {
        CurrentLives = Mathf.Max(0, CurrentLives - 1);
        stageProgressEvents.RequestLivesChanged(CurrentLives);

        if (CurrentLives <= 0)
            stageProgressEvents.RequestGameOver();
    }
}
