using UnityEngine;

public class Effectors : MonoBehaviour
{
    [SerializeField] private EffectorTargetPair[] _effectorTargetPairs;
    [SerializeField] private GameEvent _stageFinished;

    private int _efectorsDone;

    private void Awake()
    {
        foreach (EffectorTargetPair pair in _effectorTargetPairs)
        {
            pair.Init();
            pair.Effector.TargetMatch += OnEffectorReachTarget;
        }
    }

    public void SetToTargetPositions()
    {
        foreach (EffectorTargetPair pair in _effectorTargetPairs)
        {
            pair.SetToTargetPosition();
        }
    }

    private void OnEffectorReachTarget()
    {
        if ((++_efectorsDone) == _effectorTargetPairs.Length)
        {
            _stageFinished.Raise();
        }
    }

    [System.Serializable]
    private class EffectorTargetPair
    {
        [SerializeField] private Effector _effector;
        [SerializeField] private Transform _target;

        public Effector Effector => _effector;

        public void Init()
        {
            _effector.SetTarget(_target);
        }

        public void SetToTargetPosition()
        {
            _effector.transform.position = _target.position;
        }
    }
}
