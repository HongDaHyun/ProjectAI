using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipMonster : ToolTip
{
    public Text title;
    public Image img;
    public Text[] content;
    public Transform synergyPannel;

    Color color;

    public override void SetToolTip(MonsterStat stat)
    {
        switch(stat.tier)
        {
            case Tier.Common:
                ColorUtility.TryParseHtmlString("#1B9124", out color);
                break;
            case Tier.Rare:
                ColorUtility.TryParseHtmlString("#2123A9", out color);
                break;
            case Tier.Epic:
                ColorUtility.TryParseHtmlString("#A8208E", out color);
                break;
            case Tier.Legend:
                ColorUtility.TryParseHtmlString("#9A8833", out color);
                break;
        }
        title.color = color;
        title.text = stat.name;
        img.sprite = gameManager.csvReader.spriteExportData(stat.name).sprite;
        content[0].text = stat.damage.ToString();
        content[1].text = stat.hp.ToString();
        content[2].text = stat.coolTime.ToString();
        content[3].text = stat.tier.ToString();
        content[4].text = stat.type.ToString();
        content[5].text = "\"" + stat.description + "\"";
        content[6].text = string.Format("X{0}", stat.count);
        if (stat.synergy.Count > 0)
        {
            for(int i = 0; i < stat.synergy.Count; i++)
            {
                SynergyUI synergyUI = gameManager.poolingManager.MakeObj("SynergyUI").GetComponent<SynergyUI>();
                synergyUI.rect.SetParent(synergyPannel);
                synergyUI.rect.position = synergyPannel.transform.position;
                synergyUI.rect.localScale = new Vector3(1, 1, 1);
                synergyUI.SetSynergy(stat.synergy[i]);
            }
        }
    }

    private void OnDisable()
    {
        if(synergyPannel.childCount > 0)
        {
            SynergyUI[] synergyUI = synergyPannel.GetComponentsInChildren<SynergyUI>();
            for (int i = 0; i < synergyUI.Length; i++)
                synergyUI[i].gameObject.SetActive(false);
        }
    }
}
