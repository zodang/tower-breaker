using System.Collections;
using DG.Tweening;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageProgressEvents stageEvents;
    [SerializeField] private PlayerInputEvents playerInputEvents;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private FloorData[] floorData;

    [SerializeField] private FloorSlot floorSlotPrefab;
    [SerializeField] private Transform floorSlotRoot;

    public int CurrentFloor { get; private set; } = 0;

    private FloorSlot[] _floorSlots;
    private FloorSlot CurrentSlot => _floorSlots[CurrentFloor];
    private bool IsLastFloor => CurrentFloor >= _floorSlots.Length - 1;

    private bool _isPaused = false;
    private int _totalKillCount = 0;

    private void OnEnable()
    {
        stageEvents.OnPlayerDamaged += PauseEnemy;
        playerInputEvents.OnDefenseRequested += ResumeGame;
    }

    private void OnDisable()
    {
        stageEvents.OnPlayerDamaged -= PauseEnemy;
        playerInputEvents.OnDefenseRequested -= ResumeGame;
    }

    private void PauseEnemy()
    {
        _isPaused = true;
        CurrentSlot.PushAliveEnemies();
        CurrentSlot.Deactivate();
    }

    private void ResumeGame()
    {
        if (!_isPaused) return;

        CurrentSlot.Activate();
        playerMovement.ReturnOriginalPosition();

        _isPaused = false;
    }


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        CurrentFloor = 0;
        SpawnFloorSlots();

        // 현재 slot 설정
        SpawnFloor(CurrentFloor);
        CurrentSlot.Activate();

        // 다음 slot 설정
        if (HasNextFloor(1)) SpawnFloor(CurrentFloor + 1);

        SubscribeCurrentSlot();
        stageEvents.RequestFloorStarted();
        stageEvents.RequestFloorChanged(CurrentFloor + 1, floorData.Length);
    }

    private void SpawnFloorSlots()
    {
        _floorSlots = new FloorSlot[floorData.Length];

        for (int i = 0; i < floorData.Length; i++)
        {
            Vector3 spawnPos = floorSlotRoot.position + Vector3.up * (3.75f * i);
            _floorSlots[i] = Instantiate(floorSlotPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void OnFloorCleared()
    {
        UnsubscribeCurrentSlot();

        _totalKillCount += CurrentSlot.DeadEnemyCount();
        stageEvents.RequestEnemyKillCountChanged(_totalKillCount);

        if (IsLastFloor)
        {
            stageEvents.RequestStageComplete();
            return;
        }


        CurrentFloor++;

        // 다다음 슬롯 선행 스폰
        if (HasNextFloor(1)) SpawnFloor(CurrentFloor + 1);

        SubscribeCurrentSlot();
        stageEvents.RequestFloorCleared();

        StartCoroutine(FloorTransitionRoutine());
    }

    private IEnumerator FloorTransitionRoutine()
    {
        playerMovement.SetControllable(false);

        yield return playerMovement.RigidBody.DOMoveX(3f, 0.5f)
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        // 슬롯 스크롤 전 모든 적 이동 정지
        foreach (var slot in _floorSlots)
            slot.Deactivate();

        // 슬롯 스크롤 (적이 자식이므로 같이 내려감)
        foreach (var slot in _floorSlots)
            slot.transform.DOMoveY(slot.transform.position.y - 3.75f, 0.3f)
                .SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(0.3f);
        playerMovement.RigidBody.position = new Vector2(-3f, -0.1f);

        yield return playerMovement.RigidBody.DOMoveX(-1.75f, 0.3f)
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        // 층 이동 완료 후
        stageEvents.RequestFloorStarted();

        CurrentSlot.Activate();
        playerMovement.SetControllable(true);
        stageEvents.RequestFloorChanged(CurrentFloor + 1, floorData.Length);
    }

    private void SpawnFloor(int floorIndex)
    {
        if (!HasFloorData(floorIndex)) return;

        FloorSlot slot = _floorSlots[floorIndex];
        FloorData data = floorData[floorIndex];

        for (int i = 0; i < data.NormalEnemyCount; i++)
            slot.RegisterEnemy(spawner.SpawnNormalEnemy(slot.spawnPoint));

        for (int i = 0; i < data.SpeedEliteCount; i++)
            slot.RegisterEnemy(spawner.SpawnEliteEnemy(EliteEnemyType.SpeedElite, slot.spawnPoint));

        for (int i = 0; i < data.HpEliteCount; i++)
            slot.RegisterEnemy(spawner.SpawnEliteEnemy(EliteEnemyType.HpElite, slot.spawnPoint));
    }

    private void SubscribeCurrentSlot()
    {
        CurrentSlot.OnFloorCleared += OnFloorCleared;
    }

    private void UnsubscribeCurrentSlot()
    {
        CurrentSlot.OnFloorCleared -= OnFloorCleared;
    }

    private bool HasNextFloor(int offset)
    {
        return CurrentFloor + offset < _floorSlots.Length && HasFloorData(CurrentFloor + offset);
    }

    private bool HasFloorData(int index)
    {
        return index >= 0 && index < floorData.Length;
    }

}
