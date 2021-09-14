using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewEnemyUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    public TextMeshProUGUI descriptionText;

    public Image enemyImage;

    public string[] newEnemyTitle;

    public string[] newEnemyDescription;

    public Sprite[] newEnemyImage;

    public int[] waveIndex;

    public GameObject warning;

    private WaveSpawner waveSpawned;

    private Animator animator;

    private void Start()
    {
        waveSpawned = BuildManager.instance.GetComponent<WaveSpawner>();
        animator = GetComponent<Animator>();
        waveSpawned.OnWaveEnded += WaveEnd;
        waveSpawned.OnWavePriceLocked += CloseWindow;
    }
    private void OnDestroy()
    {
        waveSpawned.OnWaveEnded -= WaveEnd;
        waveSpawned.OnWavePriceLocked -= CloseWindow;
    }

    private void WaveEnd()
    {
        for (int i = 0; i < waveIndex.Length; i++)
        {
            if (waveSpawned.waveIndex + 1 == waveIndex[i])
            {
                warning.SetActive(true);
                titleText.text = newEnemyTitle[i];
                descriptionText.text = newEnemyDescription[i];
                enemyImage.sprite = newEnemyImage[i];//waveSpawned.waves[waveSpawned.waveIndex].enemies[0].enemy.GetComponentInChildren<SpriteRenderer>().sprite;
                animator.SetTrigger("Warning");
                animator.ResetTrigger("Close");
                break;
            }
        }
    }
    public void CloseWindow()
    {
        animator.SetTrigger("Close");
        animator.ResetTrigger("Warning");
    }
}
