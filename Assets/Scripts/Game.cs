using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform _waterEnter;
    [SerializeField] private Transform _water;
    [SerializeField] private Character _character;
    [SerializeField] private Stages _stages;
    [SerializeField] private GameEvent _startJump;
    [SerializeField] private GameEvent _repeatStarted;

    private Mode _gameMode = Mode.Play;

    public Mode GameMode => _gameMode;

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
}

public enum Mode
{
    Play,
    Repeat
}