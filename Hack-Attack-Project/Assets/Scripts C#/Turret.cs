using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    public bool nearestTarget = false;
    public int bulletDamage;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = 0.5f;
    public float reduceDefenceAmount = 0;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    public float laserWidth;

    [Header("Special Abileties")]
    public bool lock4Rotations;
    public float burstDuration; 
    public float burstCooldown;
    public ParticleSystem cooldownEffect;

    //public bool ChargeBCD;
    public bool keepTarget;

    public int multiTargets;
    public float multiDelay;
    public float increseFrenquencyPct;
    public bool sniper;
    public int poisonDamage;
    public float poisonTime;
    public GameObject clusterBombTarget;
    public float clusterCooldown;
    public bool splitter;
    public bool tesla;
    public Turret[] extraLasers;
    public bool stunner;

    private float cooldownTimer;
    private float burstTimer;
    private List<Bullet> clusterBullets;
    private float baseFrenquency;
    private float multiCountdown;
    private Stack<Transform> mTargets;
    private float cCooldown;
    private bool hasCleared;
    
    [Header("Upgrades")]
    public int upgradeDamage;
    public float upgradeRange;
    public float upgradeFrenquency;
    public int upgradeLaserDoT;

    [Header("Unity Setup Fields M.I.S")]
    public string enemyTag = "Enemy";
    public Transform rotationPoint;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private AudioSource audioSource;
    [SerializeField]
    private float randomPitchRangeMax;
    [SerializeField]
    private float randomPitchRangeMin;

    private void Start()
    {
        if (clusterBombTarget != null)
        {
            fireCountdown = 1f / fireRate;
            clusterBullets = new List<Bullet>();
            hasCleared = true;
        }
        if (useLaser)
        {
            lineRenderer.startWidth = laserWidth;
        }
        baseFrenquency = fireRate;
        mTargets = new Stack<Transform>();
        multiCountdown = 0;
        InvokeRepeating("UpdateTarget", 0f, 0.2f); //Update Target Delay
        BuildManager.instance.GetComponent<WaveSpawner>().OnWaveEnded += ResetRotation;
        audioSource = GetComponent<AudioSource>();

        if(burstDuration > 0)
        {
            burstTimer = burstDuration;
        }

        //if (ChargeBCD)
        //{
        //    cooldownTimer = burstCooldown;
        //}
        //else
        //{
        //    cooldownTimer = 0;
        //}
       
    }
    private void OnDestroy()
    {
        if (BuildManager.instance != null)
        {
            BuildManager.instance.GetComponent<WaveSpawner>().OnWaveEnded -= ResetRotation;
        }
    }
    void UpdateTarget()
    {
        if (clusterBombTarget != null)
        {
            UpdateEnemiesWithinRange();
            return;
        }

        if (target != null && transform.position != null && !nearestTarget)
        {
            if (!stunner || !targetEnemy.Stuned)
            {
                if (Vector2.Distance(transform.position, target.position) <= range || keepTarget)
                return;
            }
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (!stunner || !enemy.GetComponent<Enemy>().Stuned)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (splitter)
                {
                    bool extraTarget = false;
                    for (int i = 0; i < extraLasers.Length; i++)
                    {
                        if (enemy.transform == extraLasers[i].target)
                        {
                            extraTarget = true;
                            break;
                        }
                    }
                    if (distanceToEnemy < shortestDistance && !extraTarget)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                    }
                }
                else if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (increseFrenquencyPct > 0) { fireRate = baseFrenquency; }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
            if (clusterBombTarget != null)
            {
                GameObject cbEffect = Instantiate(clusterBombTarget, target.position, target.rotation, target);
            }
        }
        else
        {
            target = null;
        }
    }

    private void FindEnemiesWithinRange()
    {
        if (target == null) { return; }

        int index = multiTargets - 1;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        mTargets.Clear();

        foreach (GameObject enemy in enemies)
        {
            if (index > 0)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy <= range && enemy.transform != target && !enemy.GetComponent<Enemy>().StealthMode)
                {

                    mTargets.Push(enemy.transform);
                    if (clusterBombTarget != null)
                    {
                        GameObject cbEffect = Instantiate(clusterBombTarget, enemy.transform.position, enemy.transform.rotation, enemy.transform);
                    }
                    index--;
                }
            }
            else
            {
                break;
            }
        }
    }
    private void UpdateEnemiesWithinRange()
    {
        if (cCooldown > 0) { return; }

        int index = multiTargets;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            if (index > 0)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy <= range && !mTargets.Contains(enemy.transform) && !enemy.GetComponent<Enemy>().StealthMode)
                {
                    mTargets.Push(enemy.transform);
                    GameObject cbEffect = Instantiate(clusterBombTarget, enemy.transform.position, enemy.transform.rotation, enemy.transform);

                    GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Bullet bullet = bulletGO.GetComponent<Bullet>();

                    if (bullet != null)
                    {
                        bullet.damage = bulletDamage;
                        bullet.Seek(enemy.transform);
                        bullet.cbTarget = cbEffect;
                        if (poisonDamage > 0)
                        {
                            bullet.poisonDamage = poisonDamage;
                            bullet.poisonTime = poisonTime;
                        }
                        clusterBullets.Add(bullet);
                    }

                    bulletGO.SetActive(false);

                    index--;
                }
            }
            else
            {
                break;
            }
        }
    }
    private void Update()
    {
        if(fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
        else if (tesla)
        {
            fireCountdown = 1f / fireRate;
        }

        if (burstCooldown > 0 && cooldownTimer > 0) // && !ChargeBCD 
        {
            cooldownTimer -= Time.deltaTime;
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }
            if (cooldownTimer <= 0)
            {
                burstTimer = burstDuration;
                if (cooldownEffect != null)
                {
                    cooldownEffect.Stop();
                }
            }
        }


        if (sniper && lineRenderer.enabled)
        {
            Color lineColor = lineRenderer.endColor;
            lineColor.a -= 2 * Time.deltaTime;//(fireRate * 4) * Time.deltaTime;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            if (lineColor.a <= 0) 
            {
                lineRenderer.enabled = false; 
                if (cooldownEffect != null)
                {
                    cooldownEffect.Play();
                }
            }
        }

        if (target != null)
        {
            if (target.GetComponent<Enemy>().StealthMode || (Vector2.Distance(transform.position, target.position) > range && !keepTarget))
            {
                target = null;
            }
        }

        if (clusterBombTarget != null)
        {
            ClusterUpdate();
            return;
        }

        bool teslaReady = (tesla && !BuildManager.instance.GetComponent<WaveSpawner>().BuildMode);

        if (target == null && !teslaReady)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }
            //m?ske tilf?j cooldown effect?
            return;
        }

        if (mTargets.Count > 0)
        {
            while (mTargets.Peek() == null)
            {
                mTargets.Pop();
                if (mTargets.Count <= 0) { break; }
            }


            if (mTargets.Count > 0)
            {
                LockOnTarget(mTargets.Peek());
                if (multiCountdown <= 0f)
                {
                    Shoot(mTargets.Pop());
                    multiCountdown = multiDelay;
                }
                multiCountdown -= Time.deltaTime;
            }
        }
        else
        {
            LockOnTarget(target);
        }
        
        if (burstCooldown > 0 && cooldownTimer > 0)
        {
            //if (ChargeBCD)
            //{
            //    cooldownTimer -= Time.deltaTime;
            //    if(cooldownTimer <= 0)
            //    {
            //        burstTimer = burstDuration;
            //    }
            //}
            return;
        }
        //else if (cooldownEffect != null)
        //{
        //    cooldownEffect.Stop();
        //}

        if(burstTimer > 0)
        {
            burstTimer -= Time.deltaTime;
            if (burstTimer <= 0)
            {
                cooldownTimer = burstCooldown;
                if (cooldownEffect != null)
                {
                    cooldownEffect.Play();
                }
            }
        }

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                if (multiTargets > 0)
                {
                    FindEnemiesWithinRange();
                    multiCountdown = multiDelay;
                }
                if (increseFrenquencyPct > 0)
                {
                    if (fireRate < baseFrenquency * 3)
                    {
                        fireRate += baseFrenquency * increseFrenquencyPct;
                    }

                }
                Shoot(target);
                fireCountdown = 1f / fireRate;
            }
            //fireCountdown -= Time.deltaTime;
        }
    }
    private void ClusterUpdate()
    {
        if (target != null)
        {
            LockOnTarget(target);
        }

        if (fireCountdown <= 0f)
        {
            foreach (Bullet bullet in clusterBullets)
            {
                bullet.gameObject.SetActive(true);
            }
            clusterBullets.Clear();

            if (increseFrenquencyPct > 0)
            {
                if (fireRate < baseFrenquency * 3)
                {
                    fireRate += baseFrenquency * increseFrenquencyPct;
                }

            }

            fireCountdown = 1f / fireRate;

            cCooldown = clusterCooldown;
            hasCleared = false;

        }
        if (cCooldown <= 0f && mTargets.Count > 0 && !hasCleared)
        {
            mTargets.Clear();
            hasCleared = true;
        }
        cCooldown -= Time.deltaTime;

        if (mTargets.Count > 0)
        {
            if (target == null)
            {
                target = mTargets.Peek();
            }

            fireCountdown -= Time.deltaTime;
        }
    }
    void LockOnTarget(Transform newTraget)
    {
        if (newTraget == null)
        {
            return;
        }
        Vector3 dir = newTraget.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (lock4Rotations)
        {
            Debug.Log(angle);

            if (angle > -135 && angle < -45)
            {
                rotationPoint.rotation = Quaternion.AngleAxis(-180, Vector3.forward);
            }
            else if (angle < 135 && angle > 45)
            {
                rotationPoint.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            }
            else if (angle < 45 && angle > -45)
            {
                rotationPoint.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else
            {
                rotationPoint.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }



            return;
        }
        if (rotationPoint != null)
        {
            rotationPoint.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
    void Laser()
    {
        targetEnemy.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        targetEnemy.ReduseDefence(reduceDefenceAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * .18f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
    void Shoot(Transform bulletTarget)
    {
        if (bulletPrefab == null)
        {
            return;
        }
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        PlaySound();
        if (bullet != null)
        {
            bullet.damage = bulletDamage;
            if (tesla)
            {

                bullet.Seek(transform);
            }
            else
            {
                bullet.Seek(bulletTarget);
            }

            if (sniper)
            {
                bullet.transform.position = bulletTarget.position;
                bullet.HitTarget();
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, bulletTarget.position);
                Color lineColor = lineRenderer.endColor;
                lineColor.a = 1;
                lineRenderer.startColor = lineColor;
                lineRenderer.endColor = lineColor;
                if (cooldownEffect != null)
                {
                    cooldownEffect.Stop();
                }
            }
            if (poisonDamage > 0)
            {
                bullet.poisonDamage = poisonDamage;
                bullet.poisonTime = poisonTime;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void ResetRotation()
    {
        if (rotationPoint != null)
        {
            rotationPoint.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (cooldownEffect != null)
        {
            cooldownEffect.Stop();
        }
    }

    public void PlaySound()
    {
        audioSource.Stop();

        audioSource.pitch = Random.Range(randomPitchRangeMin, randomPitchRangeMax);

        audioSource.Play();
    }
}
