using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGrow : MonoBehaviour
{
    //[HideInInspector]
    public float explosionRange;

    private Vector2 startPosition;
    private Vector2 endPosition;

    public float growSpeed;

    private void Start()
    {
        startPosition = transform.localScale * 0.1f;
        endPosition = transform.localScale;
        transform.localScale = startPosition;
    }
    private void Update()
    {

        transform.localScale = Vector2.Lerp(transform.localScale, endPosition, growSpeed * Time.deltaTime);

        if (transform.localScale.x >= explosionRange * 2)
        {
            Destroy(gameObject);
        }
    }
}
