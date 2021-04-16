using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class PoseButton : MonoBehaviour
{
    [SerializeField] private Image _iconImage;

    private Image _background;
    private Button _button;

    public event UnityAction<PoseButton> ButtonClicked;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _background = GetComponent<Image>();
        _button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.DOScale(Vector3.one * 1.2f, 0.15f).OnComplete(() =>
        {
            rectTransform.DOScale(Vector3.one, 0.15f);
        });

        ButtonClicked?.Invoke(this);
        _button.interactable = false;
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void SetBackgroundSprite(Sprite sprite)
    {
        _background.sprite = sprite;
    }

    public void SetIconImageSprite(Sprite sprite)
    {
        _iconImage.sprite = sprite;
    }
}
