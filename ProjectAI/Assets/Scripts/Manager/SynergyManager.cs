using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void SynergyCheck()
    {
        for(int i = 0; i < gameManager.csvReader.list.synergies.Length; i++)
        {
            int z = 0;
            for (int k = 0; k < gameManager.csvReader.list.synergies[i].contents.Count; k++)
            {
                if (gameManager.csvReader.monsterExportData(gameManager.csvReader.list.synergies[i].contents[k]).num > 0)
                    z++;
            }
            if (z == gameManager.csvReader.list.synergies[i].contents.Count)
                gameManager.csvReader.list.synergies[i].isUnlock = true;
            else
                gameManager.csvReader.list.synergies[i].isUnlock = false;
        }
    }

    public void SynergyNotice()
    {
        for(int i = 0; i < gameManager.csvReader.list.synergies.Length; i++)
        {
            if(gameManager.csvReader.list.synergies[i].isUnlock)
            {
                int z = 0;
                for (int k = 0; k < gameManager.csvReader.list.synergies[i].contents.Count; k++)
                {
                    if (gameManager.uiManager.slotsUI.CheckSlot_Equip(gameManager.csvReader.list.synergies[i].contents[k]))
                    {
                        z++;
                    }
                }
                if (z == gameManager.csvReader.list.synergies[i].contents.Count && !gameManager.csvReader.list.synergies[i].isEquip)
                {
                    gameManager.uiManager.synergyNotice.gameObject.SetActive(true);
                    gameManager.uiManager.synergyNotice.SetStat(gameManager.csvReader.list.synergies[i]);
                    gameManager.csvReader.list.synergies[i].isEquip = true;
                }
                else if(z != gameManager.csvReader.list.synergies[i].contents.Count)
                {
                    gameManager.csvReader.list.synergies[i].isEquip = false;
                }
            }
            else
            {
                gameManager.csvReader.list.synergies[i].isEquip = false;
            }
            gameManager.uiManager.SynergySlotCheck(gameManager.csvReader.list.synergies[i]);
        }
    }
}
