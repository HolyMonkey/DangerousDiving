using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _force;

    private bool _isMove;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_isMove)
            Move();
    }

    public void OnlevelStart()
    {
        _isMove = true;
        _animator.SetTrigger("Run");
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + Vector3.right * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent(out Springboard board))
        {
            _rigidbody.AddForce((Vector3.up + Vector3.right).normalized * _force, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
        }
    }
}
