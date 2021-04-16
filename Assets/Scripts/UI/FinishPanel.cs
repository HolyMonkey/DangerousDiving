using UnityEngine;
using DG.Tweening;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _innerPanel;

    private void OnEnable()
    {
        _innerPanel.DOScale(Vector3.zero, 0.2f).From().SetEase(Ease.OutBack).SetDelay(.8f);
    }
}
