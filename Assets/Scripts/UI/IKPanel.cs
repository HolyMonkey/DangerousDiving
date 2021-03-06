using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class IKPanel : MonoBehaviour
{
    //Deprecated
    [SerializeField] private Button _top;
    [SerializeField] private Button _front;
    [SerializeField] private Button _side;

    [SerializeField] private RectTransform _slowPanel;
    [SerializeField] private RectTransform _slowIcon;
    [SerializeField] private IKControl _ikControlTemplate;
    [SerializeField] private RectTransform _canvas;
    [Header("Efectors")]
    [SerializeField] private Transform[] _effectors;

    private List<IKControl> _controls = new List<IKControl>();
    private IKControl _selectedControl;

    public event UnityAction<CameraPoint> CameraButtonClicked;

    private void Awake()
    {
        CreateControls();
        _slowIcon.DOLocalRotate(new Vector3(0, 0, 180), 2f).SetLoops(-1, LoopType.Restart).SetEase(Ease.InOutBack);
    }

    private void OnEnable()
    {
        _top.onClick.AddListener(delegate { OnButtonClick(CameraPoint.Top); });
        _front.onClick.AddListener(delegate { OnButtonClick(CameraPoint.Front); });
        _side.onClick.AddListener(delegate { OnButtonClick(CameraPoint.Side); });
        _slowPanel.DOScale(Vector3.one, .7f).SetEase(Ease.OutBack);
    }

    private void OnDisable()
    {
        _top.onClick.RemoveListener(delegate { OnButtonClick(CameraPoint.Top); });
        _front.onClick.RemoveListener(delegate { OnButtonClick(CameraPoint.Front); });
        _side.onClick.RemoveListener(delegate { OnButtonClick(CameraPoint.Side); });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Select();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Deselect();
        }

        if (Input.GetMouseButton(0))
        {
            if (_selectedControl != null)
            {
                _selectedControl.Move(Input.mousePosition);
            }
        }
    }

    private void Select()
    {
        foreach (IKControl control in _controls)
        {
            if (control.Rect.Contains(Input.mousePosition) && control.IsActive)
            {
                _selectedControl = control;
                control.Activate();
            }
        }
    }

    private void Deselect()
    {
        _selectedControl = null;

        foreach (IKControl control in _controls)
        {
            if (!control.IsActive)
                continue;
            
            control.Deactivate();
        }
    }

    private void CreateControls()
    {
        foreach (Transform effector in _effectors)
        {
            IKControl effectorUI = Instantiate(_ikControlTemplate, GetComponent<RectTransform>());
            effectorUI.SetEffector(effector);
            _controls.Add(effectorUI);
        }
    }

    private void ShowControls()
    {
        Camera camera = Camera.main;

        foreach (IKControl control in _controls)
        {
            Vector3 screenPosition = camera.WorldToScreenPoint(control.Effector.position);
            control.GetComponent<RectTransform>().position = screenPosition;
        }
    }

    private void OnButtonClick(CameraPoint cameraPoint)
    {
        CameraButtonClicked?.Invoke(cameraPoint);
    }

    public void OnViewOintReach()
    {
        ShowControls();
    }

    public void OnStageReached()
    {
        foreach (IKControl control in _controls)
        {
            control.ResetControl();
        }
    }

    public void OnStageFinish()
    {
        _slowPanel.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
