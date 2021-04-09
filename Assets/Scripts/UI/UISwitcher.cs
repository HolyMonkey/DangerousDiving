using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GameEvent _levelStart;
    [SerializeField] private RectTransform _slider;

    public void OnStartButtonClick()
    {
        _startButton.gameObject.SetActive(false);
        _levelStart.Raise();
    }

    public void OnStageReached()
    {
        //_slider.gameObject.SetActive(true);
        //_slider.DOScale(Vector3.zero, .5f).From();
    }

    public void OnStageFinish()
    {
        //_slider.DOScale(Vector3.zero, .5f);
        _slider.GetComponent<Slider>().value = 0;
    }
}


