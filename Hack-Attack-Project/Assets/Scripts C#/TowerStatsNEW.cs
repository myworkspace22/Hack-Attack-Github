using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStatsNEW : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public float xPos = 0;
    public float yPos = 0;
    public float zPos = -2;

    public TooltipTrigger upgradeButton, upgradeButton2, levelUpButton, sellButton, sellButton2, sellButton3;

    public Image upgradeImage, upgradeImage2, levelupImage, levelupBG, upgradeBG, upgradeBG2;

    //private BuildManager buildManager;

    //private void Start()
    //{
    //    buildManager = BuildManager.instance;
    //}

    //public void Update()
    //{
    //    if (BuildManager.instance.selectedNode != null)
    //    {
    //        UpdateTowerToolTip();
    //    }
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

    public void UpdateTowerToolTip()//TurretBluePrint blueprint, Turret getTurret = null)
    {
        UpdateLevelUpToolTip();
        UpdateUpgradeTooltip();
        UpdateSellToolTip();
    }

    private void UpdateLevelUpToolTip()//TurretBluePrint blueprint, Turret getTurret = null)
    {
        TooltipTrigger tmp = levelUpButton;
        Turret turretToUpgrade = BuildManager.instance.selectedNode.turret.GetComponent<Turret>();
        //Turret tmpTurret = getTurret != null ? getTurret : blueprint.prefab.GetComponent<Turret>();
        tmp.ShowUi = true;

        int nextLevelCost = (int)(Mathf.Pow(2, target.towerLevel + 1) * 10);
        string color = (PlayerStats.Money >= nextLevelCost) ? "#FFD500" : "#9A9A9A";
        levelupImage.color = (PlayerStats.Money >= nextLevelCost) ? new Color(1, 1, 1) : new Color(1, 1, 1, 0.25f);
        levelupBG.color = (PlayerStats.Money >= nextLevelCost) ? new Color(1, 1, 1) : new Color(0.65f, 0.65f, 0.65f);
        tmp.content = "Cost: <color=" + color + ">$" + nextLevelCost + "</color> \n" +
            "Damage: " + turretToUpgrade.bulletDamage + " <color=#00FF00>-> " + (turretToUpgrade.bulletDamage + turretToUpgrade.upgradeDamage) + "</color> \n" +
            "Range: " + turretToUpgrade.range * 100 + " <color=#00FF00>-> " + (turretToUpgrade.range + turretToUpgrade.upgradeRange) * 100 + "</color> \n" +
            "Frequency: " + turretToUpgrade.fireRate + " <color=#00FF00>-> " + (turretToUpgrade.fireRate + turretToUpgrade.upgradeFrenquency) + "</color>";

        if (TooltipSystem.Instant.curentTT == tmp)
        {
            tmp.UpdateTowerTooltipUI();
        }
    }

    private void UpdateUpgradeTooltip()
    {
        if (target.isMaxed)
            return;

        int nextLevelCost = (int)(Mathf.Pow(2, target.towerLevel + 1) * 10);

        Turret turretToUpgrade = BuildManager.instance.selectedNode.turret.GetComponent<Turret>();

        TooltipTrigger tmp = upgradeButton;
        TooltipTrigger tmp2 = upgradeButton2; 
        
        int multiplyer = (target.upgradeNr > 0) ? 2 : 1;
        
        //string currentTitle = (target.upgradeNr > 0) ? target.turretBlueprint.upgradeNames[target.upgradeNr - 1] : target.turretBlueprint.title;

        tmp.header = target.turretBlueprint.upgradeNames[target.upgradeNr + 1 * multiplyer - 1];
        tmp2.header = target.turretBlueprint.upgradeNames[target.upgradeNr + 2 * multiplyer - 1];

        string color = (PlayerStats.Money >= nextLevelCost) ? "#FFD500" : "#9A9A9A";
        upgradeImage.color = (PlayerStats.Money >= nextLevelCost) ? new Color(1, 1, 1) : new Color(1, 1, 1, 0.5f);
        upgradeBG.color = (PlayerStats.Money >= nextLevelCost) ? new Color(1, 1, 1) : new Color(0.65f, 0.65f, 0.65f);

        tmp.content = "Cost: <color=" + color + ">$" + nextLevelCost + "</color>\n" +
            target.turretBlueprint.upgradeDescription[target.upgradeNr + 1 * multiplyer - 1] + "\n" +
            "Damage: " + turretToUpgrade.bulletDamage + " <color=#00FF00>-> " + target.turretBlueprint.upgradedPrefab[target.upgradeNr + 1 * multiplyer - 1].GetComponent<Turret>().bulletDamage + "</color>\n" +
            "Range: " + turretToUpgrade.range * 100 + " <color=#00FF00>-> " + target.turretBlueprint.upgradedPrefab[target.upgradeNr + 1 * multiplyer - 1].GetComponent<Turret>().range * 100 + "</color>\n" +
            "Frequency: " + turretToUpgrade.fireRate + " <color=#00FF00>-> " + target.turretBlueprint.upgradedPrefab[target.upgradeNr + 1 * multiplyer - 1].GetComponent<Turret>().fireRate + "</color>";

        color = (PlayerStats.Money >= nextLevelCost) ? "#FFD500" : "#9A9A9A";
        upgradeImage2.color = (PlayerStats.Money >= nextLevelCost) ? new Color(1, 1, 1) : new Color(1, 1, 1, 0.5f);
        upgradeBG2.color = (PlayerStats.Money >= nextLevelCost) ? new Color(1, 1, 1) : new Color(0.65f, 0.65f, 0.65f);

        tmp2.content = "Cost: <color=" + color + ">$" + nextLevelCost + "</color>\n" +
            target.turretBlueprint.upgradeDescription[target.upgradeNr + 2 * multiplyer - 1] + "\n" +
            "Damage: " + turretToUpgrade.bulletDamage + " <color=#00FF00>-> " + target.turretBlueprint.upgradedPrefab[target.upgradeNr + 2 * multiplyer - 1].GetComponent<Turret>().bulletDamage + "</color>\n" +
            "Range: " + turretToUpgrade.range * 100 + " <color=#00FF00>-> " + target.turretBlueprint.upgradedPrefab[target.upgradeNr + 2 * multiplyer - 1].GetComponent<Turret>().range * 100 + "</color>\n" +
            "Frequency: " + turretToUpgrade.fireRate + " <color=#00FF00>-> " + target.turretBlueprint.upgradedPrefab[target.upgradeNr + 2 * multiplyer - 1].GetComponent<Turret>().fireRate + "</color>\n";

        upgradeImage.sprite = target.turretBlueprint.upgradeIcon[target.upgradeNr + 1 * multiplyer - 1];

        upgradeImage2.sprite = target.turretBlueprint.upgradeIcon[target.upgradeNr + 2 * multiplyer - 1];

        if (TooltipSystem.Instant.curentTT == tmp)
        {
            tmp.UpdateTowerTooltipUI();
        }
        else if(TooltipSystem.Instant.curentTT == tmp2)
        {
            tmp2.UpdateTowerTooltipUI();
        }
    }
    
    private void UpdateSellToolTip()
    {
        TooltipTrigger tmp = sellButton;
        TooltipTrigger tmp2 = sellButton2;
        TooltipTrigger tmp3 = sellButton3;
        Node node = BuildManager.instance.selectedNode;
        //Turret tmpTurret = getTurret != null ? getTurret : blueprint.prefab.GetComponent<Turret>();
        tmp.ShowUi = true;
        tmp2.ShowUi = true;
        tmp3.ShowUi = true;

        tmp.content = "Amount: <color=#FFD500>$" + node.SellAmount + "</color>";
        tmp2.content = "Amount: <color=#FFD500>$" + node.SellAmount + "</color>";
        tmp3.content = "Amount: <color=#FFD500>$" + node.SellAmount + "</color>";

        if (TooltipSystem.Instant.curentTT == tmp)
        {
            tmp.UpdateTowerTooltipUI();
        }
        else if (TooltipSystem.Instant.curentTT == tmp2)
        {
            tmp2.UpdateTowerTooltipUI();
        }
        else if (TooltipSystem.Instant.curentTT == tmp3)
        {
            tmp3.UpdateTowerTooltipUI();
        }
    }
}
