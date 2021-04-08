using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _force;
    [SerializeField] private GameEvent _stageReached;
    [SerializeField] private Transform _dolly;

    private bool _isMove;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _savedVelocity;
    private bool _isStage;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isStage)
            _animator.Play("translateOne", -1, _slider.normalizedValue);
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
            _isMove = false;
            _animator.SetTrigger("Jump");
        }
        else if (other.TryGetComponent(out Stage stage))
        {
            StartInteract();
        }
    }

    private void StartInteract()
    {
        Pause();
        _dolly.position = transform.position;
        _stageReached.Raise();
        _animator.speed = 0;
        _isStage = true;
    }

    private void Pause()
    {
        _savedVelocity = _rigidbody.velocity;
        _rigidbody.isKinematic = true;
    }

    private void Resume()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(_savedVelocity, ForceMode.VelocityChange);
    }
}
