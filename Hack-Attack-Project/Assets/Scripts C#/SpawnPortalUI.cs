using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortalUI : MonoBehaviour
{
    public SpawnPortal spawnPortal;

    public TooltipTrigger spawnTooltip;

    private void OnMouseOver()
    {
        if (spawnPortal.waves.Length <= spawnPortal.waveSpawner.waveIndex) { return; }

        Wave nextWave = spawnPortal.waves[spawnPortal.waveSpawner.waveIndex];
        string enemieList = "";
        for (int i = 0; i < nextWave.enemies.Length; i++)
        {
            if (nextWave.enemies[i].enemy != null)
            {
                enemieList = enemieList + nextWave.enemies[i].enemyName + " x" + nextWave.enemies[i].count + "\n";
            }
        }

        spawnTooltip.content = enemieList;
        spawnTooltip.UpdateTooltipUI();
    }
}
