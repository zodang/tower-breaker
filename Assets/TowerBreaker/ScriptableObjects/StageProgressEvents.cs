using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StageProgressEvents", menuName = "Scriptable Objects/StageProgressEvents")]
public class StageProgressEvents : ScriptableObject
{
    public event Action OnFloorStarted;
    public event Action OnFloorCleared;

    public event Action OnStageComplete;
    public event Action OnPlayerDamaged;
    public event Action<int> OnEnemyKillCountChanged;
    public event Action<int> OnLivesChanged;
    public event Action<int, int> OnFloorChanged;
    public event Action OnGameOver;

    public void RequestFloorStarted() => OnFloorStarted?.Invoke();
    public void RequestFloorCleared() => OnFloorCleared?.Invoke();
    public void RequestStageComplete() => OnStageComplete?.Invoke();
    public void RequestPlayerDamage() => OnPlayerDamaged?.Invoke();
    public void RequestEnemyKillCountChanged(int total) => OnEnemyKillCountChanged?.Invoke(total);

    public void RequestLivesChanged(int current) => OnLivesChanged?.Invoke(current);
    public void RequestFloorChanged(int current, int total) => OnFloorChanged?.Invoke(current, total);
    public void RequestGameOver() => OnGameOver?.Invoke();

    private void OnEnable()
    {
        OnFloorStarted = null;
        OnFloorCleared = null;
        OnStageComplete = null;
        OnPlayerDamaged = null;
        OnLivesChanged = null;
        OnFloorChanged = null;
        OnGameOver = null;
    }
}
