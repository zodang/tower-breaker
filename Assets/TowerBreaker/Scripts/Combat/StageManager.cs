using System.Collections;
using DG.Tweening;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private StageProgressEvents stageEvents;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private FloorSlot[] floorSlots;  // 씬에 배치된 슬롯 10개
    [SerializeField] private FloorData[] floorData;

    public int CurrentFloor { get; private set; } = 0;

    private FloorSlot CurrentSlot => floorSlots[CurrentFloor];
    private bool IsLastFloor => CurrentFloor >= floorSlots.Length - 1;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        CurrentFloor = 0;

        // 현재 슬롯 스폰 + 활성화
        SpawnFloor(CurrentFloor);
        CurrentSlot.Activate();

        // 다음 슬롯 선행 스폰 (비활성)
        if (HasNextFloor(1))
            SpawnFloor(CurrentFloor + 1);

        SubscribeCurrentSlot();
    }

    private void OnFloorCleared()
    {
        UnsubscribeCurrentSlot();

        if (IsLastFloor)
        {
            stageEvents.RequestStageComplete();
            return;
        }

        CurrentFloor++;
        CurrentSlot.Activate();

        // 다다음 슬롯 선행 스폰
        if (HasNextFloor(1))
            SpawnFloor(CurrentFloor + 1);

        SubscribeCurrentSlot();
        stageEvents.RequestFloorCleared();

        StartCoroutine(FloorTransitionRoutine());
    }

    private IEnumerator FloorTransitionRoutine()
    {
        playerMovement.SetControllable(false);

        yield return playerMovement.RigidBody.DOMoveX(3f, 0.3f)
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        // 슬롯 스크롤 전 모든 적 이동 정지
        foreach (var slot in floorSlots)
            slot.Deactivate();

        // 슬롯 스크롤 (적이 자식이므로 같이 내려감)
        foreach (var slot in floorSlots)
            slot.transform.DOMoveY(slot.transform.position.y - 3.75f, 0.3f)
                .SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(0.3f);

        // 스크롤 완료 후 현재 슬롯 적만 재활성화
        CurrentSlot.Activate();

        playerMovement.RigidBody.position = new Vector2(-3f, -0.1f);

        yield return playerMovement.RigidBody.DOMoveX(-1.75f, 0.3f)
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        playerMovement.SetControllable(true);
    }

    private void SpawnFloor(int floorIndex)
    {
        if (!HasFloorData(floorIndex)) return;

        FloorSlot slot = floorSlots[floorIndex];
        FloorData data = floorData[floorIndex];

        for (int i = 0; i < data.NormalEnemyCount; i++)
            slot.RegisterEnemy(spawner.SpawnNormalEnemy(slot.spawnPoint));

        for (int i = 0; i < data.SpeedEliteCount; i++)
            slot.RegisterEnemy(spawner.SpawnEliteEnemy(EliteEnemyType.SpeedElite, slot.spawnPoint));

        for (int i = 0; i < data.HpEliteCount; i++)
            slot.RegisterEnemy(spawner.SpawnEliteEnemy(EliteEnemyType.HpElite, slot.spawnPoint));
    }

    private void SubscribeCurrentSlot()
        => CurrentSlot.OnFloorCleared += OnFloorCleared;

    private void UnsubscribeCurrentSlot()
        => CurrentSlot.OnFloorCleared -= OnFloorCleared;

    private bool HasNextFloor(int offset)
        => CurrentFloor + offset < floorSlots.Length && HasFloorData(CurrentFloor + offset);

    private bool HasFloorData(int index)
        => index >= 0 && index < floorData.Length;
}
