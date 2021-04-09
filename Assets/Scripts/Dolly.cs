using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dolly : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string name)
    {
        _animator.Play(name);
    }
}
