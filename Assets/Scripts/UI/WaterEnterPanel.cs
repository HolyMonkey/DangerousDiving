using UnityEngine;
using DG.Tweening;

public class WaterEnterPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _innerPanel;
    [SerializeField] private RectTransform[] _scorePanels;
    [SerializeField] private GameEvent _levelFinished;


    private void OnEnable()
    {
        _innerPanel.DOScale(Vector3.zero, 0.2f).From().SetEase(Ease.OutBack).SetDelay(.3f).OnComplete(() => 
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_scorePanels[0].DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetDelay(.2f));
            sequence.Append(_scorePanels[1].DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetDelay(.2f));
            sequence.Append(_scorePanels[2].DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetDelay(.2f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            }));
        });
    }



    private void OnDisable()
    {
        _levelFinished.Raise();
    }
}
