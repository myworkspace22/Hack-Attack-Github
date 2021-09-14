using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSFX : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private float randomPitchRangeMax;
    [SerializeField]
    private float randomPitchRangeMin;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Stop();

        audioSource.pitch = Random.Range(randomPitchRangeMin, randomPitchRangeMax);

        audioSource.Play();
    }
}
