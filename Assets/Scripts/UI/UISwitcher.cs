using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GameEvent _startJump;
    [SerializeField] private RectTransform _slider;
    [SerializeField] private GameObject _waterenterPanel;
    [SerializeField] private GameObject _finishPanell;
    [SerializeField] private Game _game;

    private Slider _sliderComponent;

    private void Start()
    {
        _sliderComponent = _slider.GetComponent<Slider>();
    }

    public void OnStartButtonClick()
    {
        _startButton.gameObject.SetActive(false);
        _startJump.Raise();
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

    public void OnWaterEnter()
    {
        if (_game.GameMode == Mode.Play)
            _waterenterPanel.SetActive(true);
        else if (_game.GameMode == Mode.Repeat)
            _finishPanell.SetActive(true);
    }
}


