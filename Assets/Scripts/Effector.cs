using UnityEngine;
using UnityEngine.Events;

public class Effector : MonoBehaviour
{
    private Transform _target;
    private float _distance = .3f;

    public event UnityAction TargetMatch;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void DoMatching()
    {
        if (Vector3.Distance(transform.position, _target.position) < _distance)
        {
            TargetMatch?.Invoke();
            transform.position = _target.position;
            gameObject.layer = 2;
        }
    }

    public void ResetEffector()
    {
        gameObject.layer = 9;
    }
}
