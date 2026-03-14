using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private FloorSlot[] floorSlots;
    [SerializeField] private float floorDistance = 3.75f;

    private int _currentIndex = 0;
    private int SlotCount => floorSlots.Length;
    [SerializeField] private float[] cycleYPositions = { -3.25f, 0.5f, 4.25f };

    public FloorSlot CurrentSlot => floorSlots[GetSlotIndex(0)];
    public FloorSlot NextSlot    => floorSlots[GetSlotIndex(1)];

    private int GetSlotIndex(int offset)
    {
        return (_currentIndex + offset) % SlotCount;
    }

    public void NormalizeSlotPositions()
    {
        // 슬롯을 cycleYPositions 기준으로 정렬
        for (int i = 0; i < SlotCount; i++)
        {
            int yIdx = (i + 1) % cycleYPositions.Length;
            floorSlots[i].transform.position = new Vector3(
                floorSlots[i].transform.position.x,
                cycleYPositions[yIdx],
                floorSlots[i].transform.position.z
            );
        }
    }

    public void AdvanceFloor()
    {
        RearrangeSlots();
        _currentIndex = GetSlotIndex(1);
    }

    private void RearrangeSlots()
    {
        float[] yPositions = new float[SlotCount];
        for (int i = 0; i < SlotCount; i++)
            yPositions[i] = floorSlots[i].transform.position.y;

        for (int i = 0; i < SlotCount; i++)
        {
            int prevIdx = (i - 1 + SlotCount) % SlotCount;
            floorSlots[i].transform.position = new Vector3(
                floorSlots[i].transform.position.x,
                yPositions[prevIdx],
                floorSlots[i].transform.position.z
            );
        }
    }
}
