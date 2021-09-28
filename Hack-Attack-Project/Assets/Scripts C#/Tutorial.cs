using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int[] waveIndex;
    private WaveSpawner waveSpawner;
    public GameObject[] panels;

    public GameObject Highligter;
    public Transform[] objsToBeHighlighted;

    private void Start()
    {
        waveSpawner = BuildManager.instance.GetComponent<WaveSpawner>();
        waveSpawner.OnWaveEnded += WaveEnd;
        panels[0].SetActive(true);
    }
    private void OnDestroy()
    {
        waveSpawner.OnWaveEnded -= WaveEnd;
    }

    private void WaveEnd()
    {
        for (int i = 0; i < waveIndex.Length; i++)
        {
            if (waveSpawner.waveIndex + 1 == waveIndex[i])
            {
                panels[i + 1].SetActive(true);
                waveSpawner.isPaused = true;
                break;
            }
        }
    }
    public void CloseWindow()
    {
        waveSpawner.isPaused = false;
    }
}
