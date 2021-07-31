using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField]
    private float _fadeInTime = 0.2f;
    [SerializeField]
    private float _fadeOutTime = 0.2f;



    public Tooltip tooltip;

    private CanvasGroup canvasGroup = null;

    private static TooltipSystem _instant;
    public static TooltipSystem Instant
    {
        get
        {
            return _instant;
        }
    }

    public void Awake()
    {
        _instant = this;
        canvasGroup = GetComponent<CanvasGroup>();
        Hide(true);
    }

    public void Show(string content, string header = "")
    {
        tooltip.SetText(content, header);
        tooltip.gameObject.SetActive(true);
        canvasGroup.DOFade(1, _fadeInTime);
    }

    public void Hide(bool hideFast = false)
    {
        canvasGroup.DOFade(0, hideFast ? 0 : _fadeOutTime)/*.OnComplete(() => { tooltip.gameObject.SetActive(false); })*/;
    }
}
