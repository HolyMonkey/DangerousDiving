using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dolly : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _leftFoot;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _rightFoot;

    public Transform Body => _body;
    public Transform LeftHand => _leftHand;
    public Transform LeftFoot => _leftFoot;
    public Transform RightHand => _rightHand;
    public Transform RightFoor => _rightFoot;

    public void PlayAnimation(string name)
    {
        _animator.Play(name);
    }
}
