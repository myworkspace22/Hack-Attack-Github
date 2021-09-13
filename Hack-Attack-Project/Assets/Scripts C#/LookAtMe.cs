using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{
    public float offset;
    private WaveSpawner wavespawner;

    private void Start()
    {
        wavespawner = BuildManager.instance.GetComponent<WaveSpawner>();
    }

    private void Update()
    {
        if (wavespawner.BuildMode)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90);
        }
    }
}
