using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private PlayerInputEvents playerInputEvents;

    [SerializeField] private Button moveSkillBtn;
    [SerializeField] private Button defenseSkillBtn;
    [SerializeField] private Button attackSkillBtn;

    [SerializeField] private Button moveBtn;
    [SerializeField] private Button defenseBtn;
    [SerializeField] private Button attackBtn;

    private void Awake()
    {
        moveBtn.onClick.AddListener(()=> playerInputEvents.RequestMove());
        defenseBtn.onClick.AddListener(()=> playerInputEvents.RequestDefense());
        AddHoldListener(attackBtn, () => playerInputEvents.RequestAttackStart(), () => playerInputEvents.RequestAttackStop());
    }

    private void AddHoldListener(Button button, Action onDown, Action onUp)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();

        EventTrigger.Entry down = new EventTrigger.Entry();
        down.eventID = EventTriggerType.PointerDown;
        down.callback.AddListener((data) => { onDown(); });
        trigger.triggers.Add(down);

        EventTrigger.Entry up = new EventTrigger.Entry();
        up.eventID = EventTriggerType.PointerUp;
        up.callback.AddListener((data) => { onUp(); });
        trigger.triggers.Add(up);
    }
}
