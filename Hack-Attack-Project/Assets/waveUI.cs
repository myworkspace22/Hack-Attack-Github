using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waveUI : MonoBehaviour
{
    private WaveSpawner waveSpawner;

    private TextMeshProUGUI waveText;
    // Start is called before the first frame update
    void Start()
    {
        waveSpawner = BuildManager.instance.GetComponent<WaveSpawner>();
        waveText = GetComponent<TextMeshProUGUI>();
        waveSpawner.OnWaveEnded += UpdateText;
        UpdateText();
    }
    private void OnDestroy()
    {

        waveSpawner.OnWaveEnded -= UpdateText;
    }
    private void UpdateText()
    {
        if(waveSpawner.waveIndex + 1 <= waveSpawner.waveMaxLength)
        {
            waveText.text = $"wave {waveSpawner.waveIndex + 1} - {waveSpawner.waveMaxLength}";
        }
    }
}
