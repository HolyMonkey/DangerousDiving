using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _closePoint;
    [SerializeField] private GameEvent _cameraReady;
    [SerializeField] private GameEvent _cameraReachedViewPoint;
    [SerializeField] private CameraPoints _cameraPoints;
    [SerializeField] private IKPanel _ikPanel;

    private float _smoothSpeed = 0.5f;
    private bool _ismove = true;
    private Vector3 _zoomOutPoint;
    private Vector3 _originPosition;

    public event UnityAction<Transform> CameraReachedViewPoint;

    private enum Direction
    { 
        ZoomIn,
        ZoomOut
    }

    private Direction _currentDirection;

    private void OnEnable()
    {
        _ikPanel.CameraButtonClicked += OnChangePositionButtonClicked;
    }

    private void OnDisable()
    {
        _ikPanel.CameraButtonClicked -= OnChangePositionButtonClicked;
    }

    private void Start()
    {
        _originPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!_ismove)
            return;

        Vector3 desiredPosition = new Vector3(_character.position.x, _character.position.y + _offsetY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void OnChangePositionButtonClicked(CameraPoint cameraPoint)
    {
        Vector3 target = _cameraPoints.GetPoint(cameraPoint).position;
        StartCoroutine(Flyer(target));
    }

    public void OnReachStage()
    {
        _zoomOutPoint = transform.position;
        //Vector3 target = GetClosePointPosition();
        Vector3 target = _cameraPoints.GetPoint(CameraPoint.Front).position;
        _ismove = false;
        _currentDirection = Direction.ZoomIn;
        StartCoroutine(Flyer(target));
    }

    private Vector3 GetClosePointPosition()
    {
        //return new Vector3(_closePoint.x, _character.transform.position.y + _offsetY, _closePoint.y);
        return _character.position + new Vector3(_closePoint.x, _offsetY, _closePoint.y);
    }

    public void OnStageFinish()
    {
        _currentDirection = Direction.ZoomOut;
        StartCoroutine(Flyer(_zoomOutPoint));
    }

    private IEnumerator Flyer(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            transform.LookAt(_character.position + Vector3.up * _offsetY);
            yield return null;
        }
        transform.position = target;

        if (_currentDirection == Direction.ZoomOut)
        {
            _cameraReady.Raise();
            _ismove = true;
        }
        else if (_currentDirection == Direction.ZoomIn)
        {
            CameraReachedViewPoint?.Invoke(transform);
            _cameraReachedViewPoint.Raise();
        }
    }

    public void OnCharacterWaterEnter()
    {
        _ismove = false;
    }

    public void OnLevelFinish()
    {
        transform.position = _originPosition;
        transform.LookAt(_character);
        _ismove = true;
    }

    public void OnRepeatStart()
    {
        transform.position -= Vector3.one * 3f;
    }
}

public enum CameraPoint
{
    Front,
    Top,
    Side
}
