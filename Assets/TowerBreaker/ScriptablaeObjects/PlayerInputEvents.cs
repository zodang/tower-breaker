using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputEvents", menuName = "Scriptable Objects/PlayerInputEvents")]
public class PlayerInputEvents : ScriptableObject
{
    public event Action OnMoveRequested;
    public event Action OnDefenseRequested;
    public event Action OnAttackRequested;

    public void RequestMove()
    {
        OnMoveRequested?.Invoke();
    }

    public void RequestDefense()
    {
        OnDefenseRequested?.Invoke();
    }

    public void RequestAttack()
    {
        OnAttackRequested?.Invoke();
    }

    public void ClearAllListeners()
    {
        OnMoveRequested = null;
        OnDefenseRequested = null;
        OnAttackRequested = null;
    }
}
