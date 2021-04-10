using UnityEngine;
using DG.Tweening;

public class RepeatPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _text;

    private void OnEnable()
    {
        _text.DOScale(.6f, .3f).SetLoops(-1, LoopType.Yoyo);
    }
}
