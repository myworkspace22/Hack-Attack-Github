using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string header;
    [Multiline()]
    public string content;

    [SerializeField]
    private bool _showUi = true;
    public bool ShowUi { get => _showUi; set => _showUi = value; }

    public void UpdateTowerTooltipUI()
    {
        if (BuildManager.instance.selectedNode == null)
            return;

        if(BuildManager.instance.selectedNode.towerLevel <= 1 || BuildManager.instance.selectedNode.towerLevel == 4)
        {
            ShowIT();
        }
        else
        {
            HideIT();
        }
        
    }
    public void HideTooltipUI()
    {
        HideIT();
    }

    public void UpdateTooltipUI()
    {
            ShowIT();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowIT();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideIT();
    }

    public void OnMouseEnter()
    {
        ShowIT();
    }

    public void OnMouseExit()
    {
        HideIT();
    }


    private void ShowIT()
    {
        if (_showUi)
        {
            TooltipSystem.Instant.Show(content, header);
            TooltipSystem.Instant.curentTT = this;
        }
    }

    private void HideIT()
    {
        TooltipSystem.Instant.Hide();
        TooltipSystem.Instant.curentTT = null;
    }
}
