using UnityEngine;
using DG.Tweening;

public class Poses : MonoBehaviour
{
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _inactive;
    [SerializeField] private PoseButton _template;
    [SerializeField] private GameEvent _startJump;
    [SerializeField] private PoseObject[] _poseObjects;
    [SerializeField] private RectTransform _title;

    private RectTransform _rectTransform;

    private int _chosenPosesCount;

    public void Activate()
    {
        _chosenPosesCount = 0;

        _rectTransform = GetComponent<RectTransform>();

        _title.DOAnchorPosX(-530, 0.5f).From().SetDelay(1).SetEase(Ease.OutBounce);
        _rectTransform.DOAnchorPosY(760, 0.5f).From().SetDelay(1).SetEase(Ease.OutBounce);

        DrawPoseMenu();
    }

    private void DrawPoseMenu()
    {
        foreach (PoseObject pose in _poseObjects)
        {
            PoseButton poseButton = Instantiate(_template, transform);
            poseButton.SetBackgroundSprite(_inactive);
            poseButton.SetIconImageSprite(pose.Icon);
            poseButton.ButtonClicked += OnPoseButtonClick;
        }
    }

    private void OnPoseButtonClick(PoseButton poseButton)
    {
        poseButton.SetBackgroundSprite(_active);

        if ((++_chosenPosesCount) == 3)
        {
            _title.DOAnchorPosX(530, 0.3f).SetEase(Ease.InBack).SetDelay(.5f);
            _rectTransform.DOAnchorPosY(-760, 0.3f).SetEase(Ease.InBack).SetDelay(.5f).OnComplete(() =>
            {
                _startJump.Raise();
            });
        }
    }
}
