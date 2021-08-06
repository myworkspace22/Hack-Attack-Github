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

        shooter.color = new Color(0.1f, 0.1f, 0.1f);
        cannon.color = new Color(0.1f, 0.1f, 0.1f);
        laser.color = new Color(0.1f, 0.1f, 0.1f);
        
        if (selectedTurret != null)
        {
            if(selectedTurret.title == "Shooter")
            {
                shooter.color = new Color(0.2f, 0.2f, 0.2f);
            }else
            if (selectedTurret.title == "Missile")
            {
                cannon.color = new Color(0.2f, 0.2f, 0.2f);
            }else
            if (selectedTurret.title == "Laser")
            {
                laser.color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
    }
}
