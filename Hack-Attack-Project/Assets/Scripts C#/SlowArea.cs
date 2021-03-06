using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArea : MonoBehaviour
{
    public float slowAmount;
    public float range;
    public float damageOverTime;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);
                enemy.GetComponent<Enemy>().Slow(slowAmount);
            }
        }
    }
}
