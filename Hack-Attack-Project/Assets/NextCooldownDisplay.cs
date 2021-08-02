using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCooldownDisplay : MonoBehaviour
{
    public WaveSpawner waveTime;
    private float value;
    private float countdown;
    //public float duration;
    public Image fillImage;

    private void Start()
    {
        fillImage.fillAmount = 0f;
        countdown = waveTime.countdown;

        waveTime.OnWavePriceLocked += FillImageOnStart;

        //StartCoroutine(Timer(duration));
    }
    private void Update()
    {
        if(countdown != waveTime.countdown && waveTime.countdown < waveTime.timeBetweenWaves)
        {
            countdown = waveTime.countdown;

            value = countdown / waveTime.timeBetweenWaves;
            value = 1 - value;
            fillImage.fillAmount = value;
        }
    }
    private void OnDestroy()
    {
        waveTime.OnWavePriceLocked -= FillImageOnStart;
    }
    private void FillImageOnStart()
    {
        fillImage.fillAmount = 1;
    }
    //public IEnumerator Timer(float duration)
    //{
    //    float startTime = Time.time;
    //    float time = duration;
    //    float value = 0;

    //    while (Time.time - startTime < duration)
    //    {
    //        time -= Time.deltaTime;
    //        value = time / duration;
    //        value = 1 - value;
    //        fillImage.fillAmount = value;
    //        yield return null;
    //    }
    //}
}
