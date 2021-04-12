﻿using UnityEngine;
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

    private bool _isMove;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _savedVelocity;
    private bool _isStage;
    private string _currentStageAnimationName;
    private string _currentDollyAnimationName;
    private string _currentTriggerName;
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
            _animator.SetTrigger(_currentTriggerName);

            float spread = 0.05f;

            float horizontal = _joytick.Horizontal;
            float vertical = _joytick.Vertical;

            
            
            if (horizontal > (0.6f - spread) && vertical > (0.8f - spread))
            {
                _animator.SetTrigger("StageExit");
                _stageFinished.Raise();
                EndInteract();
            }

            _animator.SetFloat("BlendX", horizontal);
            _animator.SetFloat("BlendY", vertical);

            //_animator.Play("Trans1", -1, _slider.value);

            /*
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
            */
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
            if (_game.GameMode == Mode.Play)
            {
                _currentStageAnimationName = stage.StageAnimationName;
                _currentDollyAnimationName = stage.DollyAnimationName;
                _currentTriggerName = stage.RepeatTriggerName;
                _animator.ResetTrigger("StageExit");
                stage.Hide();
                StartInteract();
            }
            else if (_game.GameMode == Mode.Repeat)
            {
                _animator.SetTrigger(stage.RepeatTriggerName);
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
    }

    #region Decomposite

    private void StartInteract()
    {
        Pause();
        _isStage = true;
        _stageReached.Raise();
        _slider.interactable = true;
        ShowDolly();
        //_animator.speed = 0;
    }

    public void EndInteract()
    {
        _isStage = false;
        HideDolly();
        //_animator.speed = 1;
        _animator.Play(_currentDollyAnimationName);
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
        _dolly.GetComponent<Dolly>().PlayAnimation(_currentDollyAnimationName);
    }

    private void HideDolly()
    {
        _dolly.gameObject.SetActive(false);
    }
    #endregion
}
