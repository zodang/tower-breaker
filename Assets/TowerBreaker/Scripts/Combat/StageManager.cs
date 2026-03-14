using UnityEngine;

/// <summary>
/// 게임 진행 관련 기능 관리
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField] private StageProgressEvents stageEvents;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private FloorManager floorManager;
    [SerializeField] private FloorData[] floorData;

    public int CurrentFloor { get; private set; } = -1;

    private bool IsLastFloor()
    {
        return CurrentFloor >= floorData.Length - 1;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        CurrentFloor = 0;
        floorManager.NormalizeSlotPositions();

        SpawnFloor(floorManager.CurrentSlot, CurrentFloor);
        floorManager.CurrentSlot.Activate();

        if (HasFloorData(CurrentFloor + 1))
        {
            SpawnFloor(floorManager.NextSlot, CurrentFloor + 1);
        }

        SubscribeCurrentSlot();
    }

    private void OnFloorCleared()
    {
        UnsubscribeCurrentSlot();

        if (IsLastFloor())
        {
            OnStageComplete();
            return;
        }

        floorManager.AdvanceFloor();

        CurrentFloor++;
        floorManager.CurrentSlot.Activate();

        int preloadFloor = CurrentFloor + 1;
        if (HasFloorData(preloadFloor))
            SpawnFloor(floorManager.NextSlot, preloadFloor);

        SubscribeCurrentSlot();
        stageEvents.RequestFloorCleared();
    }

    private void SpawnFloor(FloorSlot slot, int floorIndex)
    {
        if (!HasFloorData(floorIndex)) return;

        FloorData data = floorData[floorIndex];
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

    private void SubscribeCurrentSlot()
        => floorManager.CurrentSlot.OnFloorCleared += OnFloorCleared;

    private void UnsubscribeCurrentSlot()
        => floorManager.CurrentSlot.OnFloorCleared -= OnFloorCleared;

    private bool HasFloorData(int index)
        => index >= 0 && index < floorData.Length;

    private void OnStageComplete()
    {
        stageEvents.RequestStageComplete();
    }
}
