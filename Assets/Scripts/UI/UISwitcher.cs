using UnityEngine;
using UnityEngine.UI;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    //[SerializeField] private GameEvent _startJump;
    [SerializeField] private RectTransform _slider;
    [SerializeField] private RectTransform _joystick;
    [SerializeField] private GameObject _waterenterPanel;
    [SerializeField] private GameObject _finishPanell;
    [SerializeField] private GameObject _repeatPanel;
    [SerializeField] private GameObject _choicePanel;
    [SerializeField] private GameObject _stagePanel;
    [SerializeField] private Game _game;

    private Slider _sliderComponent;

    private void Start()
    {
        //_sliderComponent = _slider.GetComponent<Slider>();
        //_choicePanel.SetActive(true);
    }

/*
    public void OnStartButtonClick()
    {
        _startButton.gameObject.SetActive(false);
        _startJump.Raise();
    }
*/

    public void OnStageReached()
    {
        //_sliderComponent.interactable = true;
        //_slider.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack);
        _stagePanel.SetActive(true);
    }

    public void OnStageFinish()
    {
        //_sliderComponent.interactable = false;
        //_sliderComponent.value = 0;
        //_slider.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack);
        //_stagePanel.SetActive(false);
    }

    public void OnWaterEnter()
    {
        if (_game.GameMode == Mode.Play)
            _waterenterPanel.SetActive(true);
        else if (_game.GameMode == Mode.Repeat)
        { 
            _finishPanell.SetActive(true);
            _repeatPanel.SetActive(false);
        }
    }

    public void OnRepeatStart()
    {
        _repeatPanel.SetActive(true);
    }

    public void OnJumpStarted()
    {
        //_choicePanel.SetActive(false);
    }
}


