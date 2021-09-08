using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [HideInInspector]
    public Stack<Node> newTowers;

    private Node target;

    public GameObject levelUpUI, upgradeUI, MaxedUI;

    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this;

        newTowers = new Stack<Node>();
    }

    private TurretBluePrint turretToBuild;
    [HideInInspector]
    public Node selectedNode;
    [HideInInspector]
    public Node hoverNode;

    //public NodeUI nodeUI;
    //public ShopUI shopUI;
    public TowerStatsNEW towerStatsUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    private void Update()
    {
        if((Input.GetButton("UndoX") || Input.GetButtonDown("UndoCTRL")) && Input.GetButtonDown("UndoZ") && newTowers.Count > 0)
        {
            Node towerToRemove = newTowers.Pop();
            towerToRemove.SellTurret();
            if(selectedNode == towerToRemove)
            {
                DeselectNode();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            DeselectNode();
            DeselectShopItem();
        }
    }


    public void DeselectShopItem()
    {
        //shopUI.DeselectTower();
        turretToBuild = null;
        if (hoverNode != null) { hoverNode.EndHover(); }
    }

    //private Color platformColor;
    public void SelectNode (Node node)
    {
        if (Input.GetButton("KeepBuilding"))
        {
            return;
        }
        
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        if (selectedNode != null)
        {
            selectedNode.ChangeRange(false);
            if (selectedNode.turret != null)
            {
                Debug.Log("Selected");
                //selectedNode.turret.GetComponent<Turret>().towerPlatform.color = platformColor;
                selectedNode.turret.GetComponent<Animator>().SetBool("selected", false);
            }
        }

        if (node.turret != null)
        {
            node.ChangeRange(true, node.turret.GetComponent<Turret>().range);
            //platformColor = node.turret.GetComponent<Turret>().towerPlatform.color;
            //node.turret.GetComponent<Turret>().towerPlatform.color = Color.white;
            node.turret.GetComponent<Animator>().SetBool("selected", true);
        }
        selectedNode = node;
        //shopUI.DeselectTower();
        turretToBuild = null;

        //nodeUI.SetTarget(node);
        UpdateUI();
        towerStatsUI.SetTarget(node);
        towerStatsUI.UpdateTowerToolTip();
    }
    public void DeselectNode()
    {
        if (selectedNode != null)
        {
            selectedNode.ChangeRange(false);
            if (selectedNode.turret != null)
            {
                Debug.Log("Deselected");
                //selectedNode.turret.GetComponent<Turret>().towerPlatform.color = platformColor;
                selectedNode.turret.GetComponent<Animator>().SetBool("selected", false);
            }
        }
        selectedNode = null;
        //nodeUI.Hide();
        towerStatsUI.Hide();
    }
    public void SelectTurretToBuild (TurretBluePrint turret)
    {
        turretToBuild = turret;
        //shopUI.SelectTower(turret);
        //selectedNode = null;

        DeselectNode();
    }
    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void ClearStack()
    {
        newTowers.Clear();
    }

    //public void DeselectTower()
    //{
    //    turretToBuild = null;
    //    shopUI.DeselectTower();
    //}
    public void LevelUp()
    {
        selectedNode.LevelUpTower();
        UpdateUI();
    }

    public void Upgrade(int upgradeIndex)
    {
        selectedNode.UpgradeTurret(upgradeIndex);
        UpdateUI();
    }

    public void Sell()
    {
        selectedNode.SellTurret();
        DeselectNode();
    }

    private void UpdateUI()
    {
        bool readyToUpgrade = selectedNode.towerLevel == 2 || selectedNode.towerLevel == 5;
        bool maxed = selectedNode.towerLevel > 5;
        levelUpUI.SetActive(!readyToUpgrade && !maxed);
        upgradeUI.SetActive(readyToUpgrade);
        MaxedUI.SetActive(maxed);
    }
}
