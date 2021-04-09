using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GameEvent _levelStart;
    [SerializeField] private RectTransform _slider;

    private Slider _sliderComponent;

    private void Start()
    {
        _sliderComponent = _slider.GetComponent<Slider>();
    }

    public void OnStartButtonClick()
    {
        _startButton.gameObject.SetActive(false);
        _levelStart.Raise();
    }

    public void OnStageReached()
    {
        _sliderComponent.interactable = true;
        _slider.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack);
    }

    public void OnStageFinish()
    {
        _sliderComponent.interactable = false;
        _sliderComponent.value = 0;
        _slider.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack);
    }
}


