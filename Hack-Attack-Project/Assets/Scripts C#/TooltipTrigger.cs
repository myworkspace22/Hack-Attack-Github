using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static LTDescr delay;
    public string header;
    [Multiline()]
    public string content;

    [SerializeField]
    private bool _showUi = true;
    public bool ShowUi { get => _showUi; set => _showUi = value; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowIT();
        //delay = LeanTween.delayedCall(0.5f, () =>
        //{
        //});
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //LeanTween.cancel(delay.uniqueId);
        HideIT();
    }

    public void OnMouseEnter()
    {
        ShowIT();
        //delay = LeanTween.delayedCall(0.5f, () =>
        //{
        //});
    }

    public void OnMouseExit()
    {
        //LeanTween.cancel(delay.uniqueId);
        HideIT();
    }


    private void ShowIT()
    {
        if (_showUi)
            TooltipSystem.Instant.Show(content, header);
    }

    private void HideIT()
    {
        TooltipSystem.Instant.Hide();
    }
}
