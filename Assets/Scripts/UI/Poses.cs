using UnityEngine;

public class Poses : MonoBehaviour
{
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _inactive;
    [SerializeField] private PoseMenuHolder _poseHolder;
    [SerializeField] private PoseButton _template;

    private PoseObject[] _poseObjects;

    private void OnEnable()
    {
        _poseObjects = _poseHolder.GetPoseObjects();
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
    }
}
