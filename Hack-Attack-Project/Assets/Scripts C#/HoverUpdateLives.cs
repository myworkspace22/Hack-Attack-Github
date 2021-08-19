using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUpdateLives : MonoBehaviour
{
    public TooltipTrigger endTooltip;

    private void OnMouseOver()
    {
        endTooltip.content = "<color=#00FF00>LIVES (" + PlayerStats.Lives.ToString() + ")</color>"; //alternativ "Remaining Lives: "
        endTooltip.UpdateTooltipUI();
    }
}
