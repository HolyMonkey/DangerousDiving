using UnityEngine;
using RootMotion.FinalIK;

public class Effectors : MonoBehaviour
{
    [SerializeField] private FullBodyBipedIK _ik;

    [Header("Efectors")]
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _leftFoot;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _rightFoot;

    [Header("Targets")]
    [SerializeField] private Transform _bodyTarget;
    [SerializeField] private Transform _leftHandTarget;
    [SerializeField] private Transform _leftFootTarget;
    [SerializeField] private Transform _rightHandTarget;
    [SerializeField] private Transform _rightFootTarget;

    public void SetEffectorsInTargets()
    {
        _ik.solver.IKPositionWeight = 1;

        _body.position = _bodyTarget.position;
        _leftHand.position = _leftHandTarget.position;
        _leftFoot.position = _leftFootTarget.position;
        _rightHand.position = _rightHandTarget.position;
        _rightFoot.position = _rightFootTarget.position;
    }


}
