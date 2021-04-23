using UnityEngine;
using DG.Tweening;

public class StagePanel : MonoBehaviour
{
    [SerializeField] private RectTransform _joystick;
    [SerializeField] private RectTransform _slowPanel;
    [SerializeField] private RectTransform _slowIcon;

    private void Start()
    {
        _slowIcon.DOLocalRotate(new Vector3(0, 0, 180), 2f).SetLoops(-1, LoopType.Restart).SetEase(Ease.InOutBack);
    }

    private void OnEnable()
    {
        _joystick.DOScale(Vector3.one, .6f).SetEase(Ease.OutBack);
        _slowPanel.DOScale(Vector3.one, .7f).SetEase(Ease.OutBack);
        
    }

    public void OnStageFinish()
    {
        _joystick.DOScale(Vector3.zero, .4f).SetEase(Ease.InBack);
        _joystick.GetComponent<FixedJoystick>().OnPointerUp(null);
        _slowPanel.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack).OnComplete(() =>
        { 
            gameObject.SetActive(false);
        });
    }
}
