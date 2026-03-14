using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EliteEnemyType Type;

    public float Hp;
    public float Speed;
    public float Weight;
}
