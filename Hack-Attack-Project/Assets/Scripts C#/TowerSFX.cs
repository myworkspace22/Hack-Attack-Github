using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerSFX : MonoBehaviour
{
    [SerializeField]
    private UnityEvent soundEvent;

    private AudioSource audioSource;

    [SerializeField]
    private float randomPitchRangeMax;
    [SerializeField]
    private float randomPitchRangeMin;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //soundEvent?.Invoke();
    }

    public void PlaySound()
    {
        audioSource.Stop();

        audioSource.pitch = Random.Range(randomPitchRangeMin, randomPitchRangeMax);

        audioSource.Play();
    }
}
