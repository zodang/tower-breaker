using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputEvents", menuName = "Scriptable Objects/PlayerInputEvents")]
public class PlayerInputEvents : ScriptableObject
{
    public event Action OnMoveRequested;
    public event Action OnDefenseRequested;
    public event Action OnAttackStartRequested;
    public event Action OnAttackStopRequested;

    // ControlPanel에서 호출
    public void RequestMove()
    {
        OnMoveRequested?.Invoke();
    }

    public void RequestDefense()
    {
        OnDefenseRequested?.Invoke();
    }

    public void RequestAttackStart()
    {
        OnAttackStartRequested?.Invoke();
    }

    public void RequestAttackStop()
    {
        OnAttackStopRequested?.Invoke();
    }

    public void ClearAllListeners()
    {
        OnMoveRequested = null;
        OnDefenseRequested = null;
        OnAttackStartRequested = null;
    }
}
