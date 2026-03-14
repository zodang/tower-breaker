using UnityEngine;

/// <summary>
/// 게임 진행 관련 기능 관리
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private FloorManager floorManager;
    [SerializeField] private FloorData[] floorData;

    public int CurrentFloor = -1;

    private void Start()
    {
        CurrentFloor = 0;

        SpawnFloor(floorManager.CurrentSlot, floorData[0]);
        SpawnFloor(floorManager.GetNextSlot(), floorData[1]);

        floorManager.CurrentSlot.OnFloorCleared += OnFloorCleared;
    }

    private void SpawnFloor(FloorSlot slot, FloorData data)
    {
        slot.ClearEnemies();

        for (int i = 0; i < data.NormalEnemyCount; i++)
        {
            var enemy = spawner.SpawnNormalEnemy(slot.spawnPoint);
            slot.RegisterEnemy(enemy);
        }

        for (int i = 0; i < data.SpeedEliteCount; i++)
        {
            var enemy = spawner.SpawnEliteEnemy(EliteEnemyType.SpeedElite, slot.spawnPoint);
            slot.RegisterEnemy(enemy);
        }

        for (int i = 0; i < data.HpEliteCount; i++)
        {
            var enemy = spawner.SpawnEliteEnemy(EliteEnemyType.HpElite, slot.spawnPoint);
            slot.RegisterEnemy(enemy);
        }
    }

    public void OnFloorCleared()
    {
        floorManager.CurrentSlot.OnFloorCleared -= OnFloorCleared;
        floorManager.ChangeFloorSlot();

        CurrentFloor++;

        int nextFloor = CurrentFloor + 1;
        if (nextFloor >= floorData.Length) return;

        SpawnFloor(floorManager.GetNextSlot(), floorData[nextFloor]);
        floorManager.CurrentSlot.OnFloorCleared += OnFloorCleared;

        // 플레이어 originalPos로 이동
    }
}
