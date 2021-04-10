using UnityEngine;
using DG.Tweening;

public class WaterEnterPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _innerPanel;
    [SerializeField] private GameEvent _levelFinished;

    private void OnEnable()
    {
        _innerPanel.DOScale(Vector3.zero, 0.2f).From().SetEase(Ease.OutBack).SetDelay(.3f).OnComplete(() => 
        {
            _innerPanel.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).SetDelay(.3f).OnComplete(() => 
            {
                gameObject.SetActive(false);
            });
        });
    }

    private void OnDisable()
    {
        _levelFinished.Raise();
    }
}
