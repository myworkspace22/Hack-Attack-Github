using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUpdateTowerCost : MonoBehaviour
{
    public TooltipTrigger towerTooltip;

    public int cost;

    private string color;

    private string baseContent;

    private void Start()
    {
        baseContent = towerTooltip.content;
        color = "#FFD500";
        towerTooltip.content = "Cost: <color=" + color + ">$" + cost + "</color>\n" + baseContent;
    }

    private void Update()
    {
        string tmpColor = color;
        color = (PlayerStats.Money >= cost) ? "#FFD500" : "#9A9A9A";

        if (color == tmpColor) { return; }

        towerTooltip.content = "Cost: <color=" + color + ">$" + cost + "</color>\n" + baseContent;

        towerTooltip.UpdateTooltipUI();
    }
}
