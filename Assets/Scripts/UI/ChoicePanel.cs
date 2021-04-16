using UnityEngine;

public class ChoicePanel : MonoBehaviour
{
    [SerializeField] private Poses _poses;

    private RectTransform _posesRectTransform;

    private void OnEnable()
    {
        _posesRectTransform = _poses.GetComponent<RectTransform>();
        _poses.Activate();
    }
}
