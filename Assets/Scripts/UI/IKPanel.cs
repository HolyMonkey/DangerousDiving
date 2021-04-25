using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IKPanel : MonoBehaviour
{
    [SerializeField] private Button _top;
    [SerializeField] private Button _front;
    [SerializeField] private Button _side;

    public event UnityAction<CameraPoint> CameraButtonClicked;

    private void OnEnable()
    {
        _top.onClick.AddListener(delegate { OnButtonClick(CameraPoint.Top); });
        _front.onClick.AddListener(delegate { OnButtonClick(CameraPoint.Front); });
        _side.onClick.AddListener(delegate { OnButtonClick(CameraPoint.Side); });
    }

    private void OnDisable()
    {
        _top.onClick.RemoveListener(delegate { OnButtonClick(CameraPoint.Top); });
        _front.onClick.RemoveListener(delegate { OnButtonClick(CameraPoint.Front); });
        _side.onClick.RemoveListener(delegate { OnButtonClick(CameraPoint.Side); });
    }

    private void OnButtonClick(CameraPoint cameraPoint)
    {
        CameraButtonClicked?.Invoke(cameraPoint);
    }
}
