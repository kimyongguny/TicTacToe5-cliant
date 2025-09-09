using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanalController : MonoBehaviour
{
    [SerializeField] private RectTransform panalRectTransform;

    private CanvasGroup _backgroundCanvasGroup;


    public delegate void PanelControllerHideDelegate();


    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panalRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
        panalRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panalRectTransform.localScale = Vector3.one;

        _backgroundCanvasGroup.DOFade(endValue: 0, duration: 0.3f).SetEase(Ease.Linear);
        panalRectTransform.DOScale(endValue: 0, duration: 0.3f).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                hideDelegate?.Invoke();
                Destroy(gameObject);
            });
        
    }
    protected void Shake()
    {
        panalRectTransform.DOShakeAnchorPos(0.3f);
    }
}
