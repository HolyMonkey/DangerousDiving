using UnityEngine;

public class PoseMenuHolder : MonoBehaviour
{
    [SerializeField] private PoseObject[] _poseObjects;

    public PoseObject[] GetPoseObjects()
    {
        return _poseObjects;
    }
}
