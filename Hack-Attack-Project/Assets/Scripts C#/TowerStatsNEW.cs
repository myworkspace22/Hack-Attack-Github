using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatsNEW : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public float xPos = 0;
    public float yPos = 0;
    public float zPos = -2;

    public TooltipTrigger upgradeButton, sellButton;

    //private BuildManager buildManager;

    //private void Start()
    //{
    //    buildManager = BuildManager.instance;
    //}

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();
        transform.position += new Vector3(xPos, yPos, zPos);

        ui.SetActive(true);
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
    public void UpdateLevelUpToolTip()//TurretBluePrint blueprint, Turret getTurret = null)
    {
        TooltipTrigger tmp = upgradeButton;
        Turret turretToUpgrade = BuildManager.instance.selectedNode.turret.GetComponent<Turret>();
        //Turret tmpTurret = getTurret != null ? getTurret : blueprint.prefab.GetComponent<Turret>();
        tmp.ShowUi = true;
        
        int nextLevelCost = target.turretBlueprint.levelUpCost * (target.towerLevel + 1);
        tmp.content = "Cost: <color=#FFD500>$" + nextLevelCost + "</color> \n" +
            "Damage: " + turretToUpgrade.bulletDamage + " <color=#00FF00>-> " + (turretToUpgrade.bulletDamage + turretToUpgrade.upgradeDamage * (target.towerLevel + 1)) + "</color> \n" +
            "Range: " + turretToUpgrade.range * 100 + " <color=#00FF00>-> " + (turretToUpgrade.range + turretToUpgrade.upgradeRange) * 100 + "</color> \n" +
            "Frequency: " + turretToUpgrade.fireRate + " <color=#00FF00>-> " + (turretToUpgrade.fireRate + turretToUpgrade.upgradeFrenquency) + "</color>";

        UpdateSellToolTip();
    }

    private void UpdateSellToolTip()
    {
        TooltipTrigger tmp = sellButton;
        Node node = BuildManager.instance.selectedNode;
        //Turret tmpTurret = getTurret != null ? getTurret : blueprint.prefab.GetComponent<Turret>();
        tmp.ShowUi = true;

        tmp.content = "Amount: <color=#FFD500>$" + node.SellAmount + "</color>";
    }
}
