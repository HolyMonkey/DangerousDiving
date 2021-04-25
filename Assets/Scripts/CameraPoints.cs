using UnityEngine;

public class CameraPoints : MonoBehaviour
{
    [SerializeField] private Transform _front;
    [SerializeField] private Transform _side;
    [SerializeField] private Transform _top;

    public Transform GetPoint(CameraPoint cameraPoint)
    {
        switch (cameraPoint)
        {
            case CameraPoint.Top:
                return _top;
            case CameraPoint.Side:
                return _side;
            case CameraPoint.Front:
                return _front;
        }

        return null;
    }
}
