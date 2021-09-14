using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUpdateLives : MonoBehaviour
{
    public TooltipTrigger endTooltip;

    private void OnMouseOver()
    {
        endTooltip.content = "LIVES (" + PlayerStats.Lives.ToString() + ")"; //alternativ "Remaining Lives: "
        endTooltip.UpdateTooltipUI();
    }
}
