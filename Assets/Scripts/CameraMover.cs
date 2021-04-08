using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private float _offsetY;

    private float _smoothSpeed = 0.2f;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(_character.position.x, _character.position.y + _offsetY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}
