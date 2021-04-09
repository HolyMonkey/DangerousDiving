using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _force;
    [SerializeField] private GameEvent _stageReached;
    [SerializeField] private GameEvent _stageFinished;
    [SerializeField] private Transform _dolly;

    private bool _isMove;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _savedVelocity;
    private bool _isStage;
    private string _currentStageAnimationName;
    private string _currentDollyAnimationName;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isStage)
        {
            if (_slider.value == _slider.maxValue)
            {
                _slider.interactable = false;
                _stageFinished.Raise();
                EndInteract();
            }
            else
            { 
                _animator.Play(_currentStageAnimationName, -1, _slider.value);
            }
        }
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
            _currentStageAnimationName = stage.StageAnimationName;
            _currentDollyAnimationName = stage.DollyAnimationName;
            stage.Hide();
            StartInteract();
        }
    }

    #region Decomposite

    private void StartInteract()
    {
        Pause();
        _isStage = true;
        _stageReached.Raise();
        _slider.interactable = true;
        ShowDolly();
        _animator.speed = 0;
    }

    public void EndInteract()
    {
        _isStage = false;
        Resume();
        HideDolly();
        _animator.speed = 1;
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

    private void ShowDolly()
    {
        _dolly.gameObject.SetActive(true);
        _dolly.position = transform.position;
        _dolly.GetComponent<Dolly>().PlayAnimation(_currentDollyAnimationName);
    }

    private void HideDolly()
    {
        _dolly.gameObject.SetActive(false);
    }
    #endregion
}
