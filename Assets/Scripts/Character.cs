using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Slider _slider;
    [SerializeField] private FixedJoystick _joytick;
    [SerializeField] private float _force;
    [SerializeField] private GameEvent _stageReached;
    [SerializeField] private GameEvent _stageFinished;
    [SerializeField] private GameEvent _waterEnter;
    [SerializeField] private Transform _dolly;
    [SerializeField] private Game _game;
    [SerializeField] private ParticleSystem _wind;

    private bool _isMove;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _savedVelocity;
    private bool _isStage;
    private Stage _currentStage;
    private Vector3 _originPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _originPosition = transform.position;
    }

    private void Update()
    {
        if (_isStage)
        {
            _animator.SetTrigger("StageEnter");

            float spread = 0.05f;

            float horizontal = _joytick.Horizontal;
            float vertical = _joytick.Vertical;

            if (horizontal > (_currentStage.JoyStickValues.x - spread) && vertical > (_currentStage.JoyStickValues.y - spread))
            {
                horizontal = 0;
                vertical = 0;

                _stageFinished.Raise();
                EndInteract();
            }

            _animator.SetFloat("BlendX", horizontal);
            _animator.SetFloat("BlendY", vertical);
        }
    }

    private void FixedUpdate()
    {
        if (_isMove)
            Move();
    }

    public void OnStartJump()
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
            _currentStage = stage;

            if (_game.GameMode == Mode.Play)
            {
                stage.Hide();
                StartInteract();
            }
            else if (_game.GameMode == Mode.Repeat)
            {
                _animator.SetTrigger("Repeat");
                _wind.Play();
            }
        }
        else if (other.TryGetComponent(out Water water))
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        _waterEnter.Raise();
        _isMove = false;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _originPosition;
        _animator.SetTrigger("Reset");
        _animator.ResetTrigger("StageEnter");
        _wind.Stop();
    }

    #region Decomposite

    private void StartInteract()
    {
        Pause();
        _isStage = true;
        _stageReached.Raise();
        _slider.interactable = true;
        ShowDolly();
        _wind.Stop();
    }

    public void EndInteract()
    {
        _isStage = false;
        HideDolly();
        _animator.Play(_currentStage.DollyAnimationName);
        _wind.Play();
    }

    private void Pause()
    {
        _savedVelocity = _rigidbody.velocity;
        _rigidbody.isKinematic = true;
    }

    public void Resume()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(_savedVelocity, ForceMode.VelocityChange);
    }

    private void ShowDolly()
    {
        _dolly.gameObject.SetActive(true);
        _dolly.position = transform.position;
        _dolly.GetComponent<Dolly>().PlayAnimation(_currentStage.DollyAnimationName);
    }

    private void HideDolly()
    {
        _dolly.gameObject.SetActive(false);
    }
    #endregion
}
