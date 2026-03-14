using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StageProgressEvents", menuName = "Scriptable Objects/StageProgressEvents")]
public class StageProgressEvents : ScriptableObject
{
    public event Action OnFloorCleared;
    public event Action OnStageComplete;

    public void RequestFloorCleared() => OnFloorCleared?.Invoke();
    public void RequestStageComplete() => OnStageComplete?.Invoke();

    private void OnEnable()
    {
        OnFloorCleared = null;
        OnStageComplete = null;
    }
}
