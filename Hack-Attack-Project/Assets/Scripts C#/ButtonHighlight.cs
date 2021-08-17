using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour
{
    public Image shooter, cannon, laser;

    private BuildManager buildmanager;

    private TurretBluePrint selectedTurret;

    private void Start()
    {
        buildmanager = BuildManager.instance;
    }
    private void Update()
    {
        if(selectedTurret == buildmanager.GetTurretToBuild())
        {
            return;
        }
        
        selectedTurret = buildmanager.GetTurretToBuild();

        shooter.color = new Color(0.10f, 0.10f, 0.10f);
        cannon.color = new Color(0.10f, 0.10f, 0.10f);
        laser.color = new Color(0.10f, 0.10f, 0.10f);

        if (selectedTurret != null)
        {
            if(selectedTurret.title == "Shooter")
            {
                shooter.color = new Color(0.15f, 0.15f, 0.15f);
            }else
            if (selectedTurret.title == "Missile")
            {
                cannon.color = new Color(0.15f, 0.15f, 0.15f);
            }
            else
            if (selectedTurret.title == "Laser")
            {
                laser.color = new Color(0.15f, 0.15f, 0.15f);
            }
        }
    }
}
