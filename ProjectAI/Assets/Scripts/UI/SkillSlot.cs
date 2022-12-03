using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : Slot
{
    public bool isCool;

    private void Update()
    {
        for(int i = 0; i < gameManager.playerAI.stat.skill.Length; i++)
            Skilling(gameManager.playerAI.stat.skill[i]);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        if (!preSlot.isSkill)
            dd.Replace();
        skillStat = eventData.pointerDrag.GetComponent<SkillUI>().stat;
    }

    public override void SwapUI(DragDrop dd)
    {
        base.SwapUI(dd);
        preSlot.skillStat = skillStat;
    }

    private void Skilling(SkillStat skill)
    {
        if(skill.name == skillStat.name)
        {
            if (isCool && !skill.isActive)
            {
                img.raycastTarget = true;
                img.fillAmount -= 1 / skill.coolTime * Time.deltaTime;

                if (img.fillAmount <= 0)
                {
                    img.fillAmount = 0;
                    img.raycastTarget = false;
                    isCool = false;
                }
            }
        }
    }
}
