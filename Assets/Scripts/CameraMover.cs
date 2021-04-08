using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _closePoint;

    private float _smoothSpeed = 0.2f;
    private bool _ismove = true;

    private void FixedUpdate()
    {
        if (!_ismove)
            return;

        Vector3 desiredPosition = new Vector3(_character.position.x, _character.position.y + _offsetY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void OnReachStage()
    {
        Vector3 target = new Vector3(_closePoint.x, _character.transform.position.y + _offsetY, _closePoint.y);
        _ismove = false;
        StartCoroutine(Flyer(target));
    }

    private IEnumerator Flyer(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            transform.LookAt(_character);
            yield return null;
        }
    }
}
