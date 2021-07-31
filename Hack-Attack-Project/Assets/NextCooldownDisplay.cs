using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCooldownDisplay : MonoBehaviour
{
    public float duration;
    public Image fillImage;

    private void Start()
    {
        fillImage.fillAmount = 1f;
        StartCoroutine(Timer(duration));
    }
    public IEnumerator Timer(float duration)
    {
        float startTime = Time.time;
        float time = duration;
        float value = 0;

        while (Time.time - startTime < duration)
        {
            time -= Time.deltaTime;
            value = time / duration;
            fillImage.fillAmount = value;
            yield return null;
        }
    }
}
