using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pax : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();

        float offset = Random.Range(0, 1f);
        _animator.SetFloat("Offset", offset);

        int animIndex = Random.Range(1, 6);
        _animator.SetFloat("Rand", animIndex);
    }
}
