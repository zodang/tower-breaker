using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
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
        attackBtn.onClick.AddListener(()=> playerInputEvents.RequestAttack());
    }
}
