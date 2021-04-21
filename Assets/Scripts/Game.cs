using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform _waterEnter;
    [SerializeField] private Transform _water;
    [SerializeField] private Character _character;
    [SerializeField] private Stages _stages;
    [SerializeField] private GameEvent _startJump;
    [SerializeField] private GameEvent _repeatStarted;
    [SerializeField] private Transform _cursor;
    [SerializeField] private CameraMover _camera;

    private Mode _gameMode = Mode.Play;

    public Mode GameMode => _gameMode;

    private Vector3 _originCursorEulers;

    private void OnEnable()
    {
        _startJump.Raise();
        _originCursorEulers = _cursor.rotation.eulerAngles;
        _camera.CameraReachedViewPoint += OnCameraReachViewPoint;
    }

    private void OnDisable()
    {
        _camera.CameraReachedViewPoint -= OnCameraReachViewPoint;
    }

    public void OnCharacterWaterEnter()
    {
        _waterEnter.position = new Vector3(_character.transform.position.x, _water.position.y, _character.transform.position.z);
        _waterEnter.gameObject.SetActive(true);
        _stages.Activate();
    }

    public void OnLevelFinish()
    {
        StartRepeat();
    }

    private void StartRepeat()
    {
        if (GameMode == Mode.Repeat)
            return;

        _waterEnter.gameObject.SetActive(false);
        _gameMode = Mode.Repeat;
        _startJump.Raise();
        _repeatStarted.Raise();
    }

    private void OnCameraReachViewPoint(Transform cameraTransform)
    {
        Vector3 newRotation = new Vector3(_cursor.rotation.eulerAngles.x + cameraTransform.rotation.eulerAngles.x, cameraTransform.rotation.eulerAngles.y, _cursor.rotation.eulerAngles.z);
        _cursor.transform.eulerAngles = newRotation;
        _cursor.gameObject.SetActive(true);
    }

    public void HideCursor()
    {
        _cursor.gameObject.SetActive(false);
        _cursor.transform.eulerAngles = _originCursorEulers;
    }
}

public enum Mode
{
    Play,
    Repeat
}