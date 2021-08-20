using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public WaveSpawner waveSpawner;

    public Wave[] waves;


    public IEnumerator SpawnWave(int waveIndex)
    {
        //nameOfLevelUI.text = nameOfLevel + " (wave: " + (waveIndex + 1) + " - " + waves.Length + ")";

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            WaveSpawner.EnemiesAlive += wave.enemies[i].count;
        }

        //enemyName.text = "Incoming: <color=#00FF00>" + wave.enemies[0].enemy.GetComponent<Enemy>().startHealth + " HP</color>";

        //enemyImage.sprite = wave.enemies[0].enemy.GetComponentInChildren<SpriteRenderer>().sprite;

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            for (int j = 0; j < wave.enemies[i].count; j++)
            {
                if (wave.enemies[i].enemy != null)
                {
                    SpawnEnemy(wave.enemies[i].enemy);
                }
                else
                {
                    WaveSpawner.EnemiesAlive--;
                }
                //if(j < wave.enemies[i].count - 1)
                //{
                yield return new WaitForSeconds(1f / wave.enemies[i].rate);
                //}
            }
        }

        waveSpawner.PortalWaweEnded();

    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject e = Instantiate(enemy, transform.position, transform.rotation);
        e.GetComponent<AIDestinationSetter>().target = waveSpawner.endPoint;
    }
}
