using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dolly : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAnimation(string name)
    {
        _animator.Play(name);
    }
}
