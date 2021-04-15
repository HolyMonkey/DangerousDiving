using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
