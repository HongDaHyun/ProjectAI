using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop_Skill : DragDrop
{
    SkillUI skillUI;

    protected override void Awake()
    {
        base.Awake();
        skillUI = GetComponent<SkillUI>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        skillUI.stat = new SkillStat();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        preSlot.skillStat = new SkillStat();
    }

    public override void Replace()
    {
        base.Replace();
        preSlot.skillStat = skillUI.stat;
    }
}
