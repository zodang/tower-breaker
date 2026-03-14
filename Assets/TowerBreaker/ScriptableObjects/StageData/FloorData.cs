using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class FloorData : ScriptableObject
{
    public int FloorIdx;

    public int NormalEnemyCount;
    public int SpeedEliteCount;
    public int HpEliteCount;
}
