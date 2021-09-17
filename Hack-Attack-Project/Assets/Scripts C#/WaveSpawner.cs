using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;
using System;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    //public TextMeshProUGUI enemyName;
    //public Image enemyImage;
    public GameObject arrowPath;
    private GameObject currentArrow;
    public bool arrowPathDeactive;
    [HideInInspector]
    public int gameSpeed;
    [HideInInspector]
    public bool isPaused;

    public TextMeshProUGUI speedButtonText;

    //public int enCount; //til at kunne se hvor mange enemies der er i banen

    public GameManager gameManager;

    public SpawnPortal[] spawnPortals;

    //public Wave[] waves; 

    //public Transform[] spawnPoints;
    public Transform endPoint;

    public float timeBetweenWaves = 5f;
    public float countdown = 10f;

    public TextMeshProUGUI waveCountdownText;

    public int waveIndex = 0;

    private bool waveEnded;

    public event Action OnWavePriceLocked;
    public event Action OnWaveEnded;

    public string nameOfLevel;
    //public TextMeshProUGUI nameOfLevelUI;

    public TextMeshProUGUI timeBonusText;

    public bool BuildMode { get { return EnemiesAlive <= 0; } }

    public int portalsReady;

    private void Start()
    {
        //nameOfLevelUI.text = nameOfLevel + " (wave: " + (waveIndex) + " - " + waves.Length + ")";
        currentArrow = null;
        gameSpeed = 1;
        isPaused = false;
        EnemiesAlive = 0;
        portalsReady = spawnPortals.Length;
    }

    private void Update()
    {
        
        //enCount = EnemiesAlive; //til at kunne se hvor mange enemies der er i banen
        if (Input.GetKeyDown("space")/* && BuildMode*/)
        {
            if (!BuildMode)
            {
                SpeedUp();
            }
            else
            {
                ReadyUp();
            }
        }
        if (isPaused && currentArrow != null)
        {
            Destroy(currentArrow);
        }
        if (BuildMode && currentArrow == null && !arrowPathDeactive && !isPaused)
        {
            currentArrow = Instantiate(arrowPath, spawnPortals[UnityEngine.Random.Range(0, spawnPortals.Length)].transform.position, spawnPortals[UnityEngine.Random.Range(0, spawnPortals.Length)].transform.rotation);
            currentArrow.GetComponent<AIDestinationSetter>().target = endPoint;
        }

        if (EnemiesAlive < GameObject.FindGameObjectsWithTag("Enemy").Length)
        {
            Debug.LogWarning("to few enemies");
            EnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        if (EnemiesAlive > 0)
        {
            waveEnded = false;
            return;
        }
        else if (!waveEnded)
        {
            Time.timeScale = 1;
            waveEnded = true;
            waveCountdownText.color = Color.white;
            OnWaveEnded?.Invoke();
        }
        int waveMaxlength = 0;
        for (int i = 0; i < spawnPortals.Length; i++)
        {
            if (spawnPortals[i].waves.Length > waveMaxlength)
            {
                waveMaxlength = spawnPortals[i].waves.Length;
            }
        }
        if (waveIndex == waveMaxlength)
        {
            if(PlayerStats.Lives > 0)
            {
                gameManager.WinLevel();
            }
            this.enabled = false;
            return;
        }
        if (countdown <= 0f)
        {
            BuildManager.instance.DeselectNode();
            Time.timeScale = gameSpeed;
            waveCountdownText.color = Color.gray;
            SpawnWave();

            //PlayerStats.Money += (PlayerStats.Money - PlayerStats.Money % 100) / 5;
            return;
        }
        //if (false)//waveIndex > 0 && !isPaused)
        //{
        //    countdown -= Time.deltaTime;

        //    countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        //    if (countdown > 10)
        //    {
        //        waveCountdownText.text = string.Format("NEXT WAVE", countdown);
        //        //waveCountdownText.text = string.Format("NEXT WAVE ({0:0})", countdown);
        //    }
        //    if (countdown <= 10)
        //    {
        //        waveCountdownText.text = string.Format("NEXT WAVE", countdown);
        //        //waveCountdownText.text = string.Format("NEXT WAVE ({0:0})", countdown);
        //    }
        //} else
        //{
        //    waveCountdownText.text = "NEXT WAVE";
        //}
        waveCountdownText.text = "NEXT WAVE";

        //enemyName.text = "Next wave: <color=#00FF00>" + waves[waveIndex].enemies[0].enemy.GetComponent<Enemy>().startHealth + " HP</color>";

        //enemyImage.sprite = waves[waveIndex].enemies[0].enemy.GetComponentInChildren<SpriteRenderer>().sprite;

        //enemyImage.color = waves[waveIndex].enemies[0].enemy.GetComponentInChildren<SpriteRenderer>().color;

        //if(waveIndex == waves.Length - 1)
        //{
        //    enemyName.text = "Final Boss: <color=#00FF00>" + waves[waveIndex].enemies[0].enemy.GetComponent<Enemy>().startHealth + " HP</color>";
        //}
    }

    private void SpawnWave()
    {
        portalsReady = 0;

        for (int i = 0; i < spawnPortals.Length; i++)
        {
            StartCoroutine(spawnPortals[i].SpawnWave(waveIndex));
        }

        BuildManager.instance.DeselectShopItem();

        Destroy(currentArrow);

        PlayerStats.Rounds++;

        OnWavePriceLocked?.Invoke();

        BuildManager.instance.ClearStack();

        countdown = timeBetweenWaves;
    }
    public void PortalWaweEnded()
    {
        portalsReady++;
        if (portalsReady >= spawnPortals.Length)
        {
            waveIndex++;
        }
    }

    //IEnumerator SpawnWave()
    //{
    //    //nameOfLevelUI.text = nameOfLevel + " (wave: " + (waveIndex + 1) + " - " + waves.Length + ")";

    //    BuildManager.instance.DeselectShopItem();

    //    Destroy(currentArrow);

    //    PlayerStats.Rounds++;

    //    OnWavePriceLocked?.Invoke();

    //    BuildManager.instance.ClearStack();

    //    Wave wave = waves[waveIndex];
        
    //    for (int i = 0; i < wave.enemies.Length; i++)
    //    {
    //        EnemiesAlive += wave.enemies[i].count;
    //    }

    //    //enemyName.text = "Incoming: <color=#00FF00>" + wave.enemies[0].enemy.GetComponent<Enemy>().startHealth + " HP</color>";

    //    //enemyImage.sprite = wave.enemies[0].enemy.GetComponentInChildren<SpriteRenderer>().sprite;

    //    for (int i = 0; i < wave.enemies.Length; i++)
    //    {
    //        for (int j = 0; j < wave.enemies[i].count; j++)
    //        {
    //            if (wave.enemies[i].enemy != null)
    //            {
    //                SpawnEnemy(wave.enemies[i].enemy);
    //            }
    //            //if(j < wave.enemies[i].count - 1)
    //            //{
    //                yield return new WaitForSeconds(1f / wave.enemies[i].rate);
    //            //}
    //        }
    //    }

    //    waveIndex++;
    //}
    //void SpawnEnemy(GameObject enemy)
    //{
    //    GameObject e = Instantiate(enemy, spawnPoints[UnityEngine.Random.Range(0, 4)].position, spawnPoints[UnityEngine.Random.Range(0, 4)].rotation);
    //    e.GetComponent<AIDestinationSetter>().target = endPoint;
    //}

    public void ReadyUp()
    {
        if (!BuildMode)
        {
            //SpeedUp();
            return;
        }
        BuildManager.instance.DeselectNode();
        //AddTimeBonus((int)countdown);
        countdown = 0;
        Time.timeScale = gameSpeed;
        //speedButtonText.text = "x" + Time.timeScale;
    }
    //public void SpaceToReadyUp()
    //{
    //    BuildManager.instance.DeselectNode();
    //    //AddTimeBonus((int)countdown);
    //    countdown = 0;
    //    Time.timeScale = gameSpeed;
    //    speedButtonText.text = "x" + Time.timeScale;
    //}

    private void AddTimeBonus(int bonus)
    {
        if(bonus < 2)
        {
            return;
        }

        PlayerStats.Money += bonus;

        timeBonusText.text = "TIME BONUS <color=#FFD500>$" + bonus + "</color>";

        timeBonusText.gameObject.GetComponent<Animator>().SetTrigger("PlayBonus");
    }

    public void SpeedUp()
    {

        if (gameSpeed >= 3)
        {
            gameSpeed = 1;
        }
        else
        {
            gameSpeed += 1;
        }

        if (!BuildMode)
        {
            Time.timeScale = gameSpeed;
        }
        speedButtonText.text = "x" + gameSpeed;
    }
}
