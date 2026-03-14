using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private FloorSlot[] floorSlots;
    [SerializeField] private float floorDistance = 3.75f;
    public FloorSlot CurrentSlot => GetCurrentSlot();
    public FloorSlot GetNextSlot()
    {
        return floorSlots[1];
    }

    public FloorSlot GetCurrentSlot()
    {
        return floorSlots[0];
    }

    public void ChangeFloorSlot()
    {
        MoveFloorSlots();
        RotateSlots();
    }

    private void MoveFloorSlots()
    {
        foreach (var slot in floorSlots)
        {
            slot.transform.position += Vector3.down * floorDistance;
        }
    }

    private void RotateSlots()
    {
        FloorSlot first = floorSlots[0];

        for(int i=0;i<floorSlots.Length-1;i++)
        {
            floorSlots[i] = floorSlots[i+1];
        }

        floorSlots[floorSlots.Length-1] = first;
    }
}
