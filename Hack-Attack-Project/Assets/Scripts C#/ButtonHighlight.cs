using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour
{
    public Image shooter, cannon, laser;
    public Image shooterIcon, cannonIcon, laserIcon;

    private BuildManager buildmanager;

    private TurretBluePrint selectedTurret;

    private void Start()
    {
        buildmanager = BuildManager.instance;
        buildmanager.GetComponent<WaveSpawner>().OnWavePriceLocked += DisableButtons;
        buildmanager.GetComponent<WaveSpawner>().OnWaveEnded += EnableButtons;
    }
    private void DisableButtons()
    {
        shooter.color = new Color(0.08f, 0.08f, 0.08f);
        cannon.color = new Color(0.08f, 0.08f, 0.08f);
        laser.color = new Color(0.08f, 0.08f, 0.08f);
        shooterIcon.color = new Color(1, 1, 1, 0.5f);
        cannonIcon.color = new Color(1, 1, 1, 0.5f);
        laserIcon.color = new Color(1, 1, 1, 0.5f);
    }
    private void EnableButtons()
    {
        shooter.color = new Color(0.08f, 0.08f, 0.08f);
        cannon.color = new Color(0.08f, 0.08f, 0.08f);
        laser.color = new Color(0.08f, 0.08f, 0.08f);
        shooterIcon.color = new Color(1, 1, 1);
        cannonIcon.color = new Color(1, 1, 1);
        laserIcon.color = new Color(1, 1, 1);
    }
    private void Update()
    {
        if (PlayerStats.Money < 20)
        {
            DisableButtons();
            return;
        }
        if (!buildmanager.GetComponent<WaveSpawner>().BuildMode)
        {
            return;
        }

        if (selectedTurret == buildmanager.GetTurretToBuild())
        {
            return;
        }
        
        selectedTurret = buildmanager.GetTurretToBuild();

        shooter.color = new Color(0.08f, 0.08f, 0.08f);
        cannon.color = new Color(0.08f, 0.08f, 0.08f);
        laser.color = new Color(0.08f, 0.08f, 0.08f);
        shooterIcon.color = new Color(1, 1, 1);
        cannonIcon.color = new Color(1, 1, 1);
        laserIcon.color = new Color(1, 1, 1);

        if (selectedTurret != null)
        {
            if(selectedTurret.title == "Shooter")
            {
                shooter.color = new Color(0.12f, 0.12f, 0.12f);
            }else
            if (selectedTurret.title == "Missile")
            {
                cannon.color = new Color(0.12f, 0.12f, 0.12f);
            }
            else
            if (selectedTurret.title == "Laser")
            {
                laser.color = new Color(0.12f, 0.12f, 0.12f);
            }
        }
    }
}
