using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    //[SerializeField] private Slider _slider;
    [SerializeField] private FixedJoystick _joytick;
    [SerializeField] private float _force;
    [SerializeField] private GameEvent _stageReached;
    [SerializeField] private GameEvent _stageFinished;
    [SerializeField] private GameEvent _waterEnter;
    [SerializeField] private Transform _dolly;
    [SerializeField] private Game _game;
    [SerializeField] private ParticleSystem _wind;
    [SerializeField] private Effects _effects;
    [SerializeField] private Volume _volume;
    [SerializeField] private Effectors _effectors;

    private float _currentSpeed;
    private bool _isMove;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _savedVelocity;
    private bool _isStage;
    private Stage _currentStage;
    private Vector3 _originPosition;
    private Dolly _dollyComponent;
    
    private FilmGrain _grain;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _originPosition = transform.position;
        _currentSpeed = _speed;
        _volume.profile.TryGet<FilmGrain>(out _grain);
        _dollyComponent = _dolly.GetComponent<Dolly>();
    }

    private void Update()
    {
        /*
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
                _effects.PoseSetup(_currentStage.Index); 

                EndInteract();
            }

            _animator.SetFloat("BlendX", horizontal);
            _animator.SetFloat("BlendY", vertical);
        }
        */
    }

    private void FixedUpdate()
    {
        if (_isMove)
            Move();
    }

    public void OnStartJump()
    {
        _isMove = true;
        //_animator.SetTrigger("Run");
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + Vector3.right * _currentSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Jump jump))
        {
            _currentSpeed = 1.5f;
            _rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
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
        _currentSpeed = _speed;
    }

    public void SetNoise()
    {
        Time.timeScale = 0.7f;
        _grain.intensity.value = 0.5f;
        //_grain.response.value = 0.75f;
    }

    

    #region Decomposite

    private void StartInteract()
    {
        Pause();
        _isStage = true;
        _isMove = false;

        _animator.enabled = false;
        ShowDolly();
        _stageReached.Raise();
        _wind.Stop();
    }

    public void EndInteract()
    {
        _isStage = false;
        _isMove = true;
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
        _dolly.position = transform.position;
        _effectors.SetEffectorsInTargets();
        _dolly.GetComponent<Dolly>().PlayAnimation(_currentStage.DollyAnimationName);
        _dolly.gameObject.SetActive(true);
    }

    private void HideDolly()
    {
        _dolly.gameObject.SetActive(false);
    }
    #endregion
}
