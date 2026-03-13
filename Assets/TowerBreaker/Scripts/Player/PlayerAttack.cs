using UnityEngine;

/// <summary>
/// 공격관련 기능
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    public void AttackStart()
    {
        Debug.Log("공격 시작");
    }

    public void AttackStop()
    {
        Debug.Log("공격 중지");
    }
}
