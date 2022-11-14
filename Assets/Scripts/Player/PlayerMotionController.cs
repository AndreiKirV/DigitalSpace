using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotionController : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private PlayerBottom _bottom;
    private PlayerInput _inputController;
    private Rigidbody _rigidBody;
    private float _targetSpeedMove = 25f;
    private float _minSpeed = 15f;
    private float _turnSpeed = 500f;
    private float _jumpForce = 15f;
    private float _gravitationalForce = 0.5f;
    private float _deltaGravitation = 1.3f;
    private Vector2 _rotation;
    private Vector3 _startScale;
    private Vector3 _sizeInSquat = new Vector3(1f,1f,1f);
    private bool _onGround = false;

    public UnityEvent IsJump = new UnityEvent();
    public UnityEvent IsSteps = new UnityEvent();
    public UnityEvent StopSteps = new UnityEvent();
    public UnityEvent ClickAction = new UnityEvent();

    private void Awake() 
    {
        _startScale = transform.localScale;

        _inputController = new PlayerInput();
        _inputController.Player.Jump.performed += context => TryJump();
        _inputController.Player.Move.performed += context => ResetSpeed(_onGround);
        _inputController.Player.Move.performed += context => EventInvoke(IsSteps);
        _inputController.Player.Move.canceled += context => ResetSpeed(_onGround);
        _inputController.Player.Move.canceled += context => EventInvoke(StopSteps);
        _inputController.Player.SitDown.performed += context => SitDown();
        _inputController.Player.SitDown.canceled += context => transform.localScale = _startScale;
        _inputController.Player.Action.performed += context => EventInvoke(ClickAction);
        _rigidBody = GetComponent<Rigidbody>();

        _bottom.LeftTheGround.AddListener(delegate 
        {
            ChangeSatus(ref _onGround, false);
            EventInvoke(StopSteps);
        });

        _bottom.WentToGround.AddListener(delegate 
        {
            ChangeSatus(ref _onGround, true);
            ResetSpeed(_onGround);
            EventInvoke(IsSteps);
        });
    }

    private void Start() 
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void OnEnable() 
    {
        _inputController.Enable();
    }

    private void OnDisable() 
    {
        _inputController.Disable();
    }

    private void FixedUpdate() 
    {
        Move();
        TurnHead();

        if(!_onGround)
        {
            _rigidBody.velocity += new Vector3(0, -_gravitationalForce*_deltaGravitation,0);
        }
    }

    private void EventInvoke(UnityEvent targetEvent)
    {
        if(targetEvent != null)
        targetEvent.Invoke();
    }

    private void ResetSpeed(bool condition)
    {
        if(condition)
        _rigidBody.velocity = Vector3.zero;
    }

    private void ChangeSatus(ref bool targetBool, bool targetValue)
    {
        targetBool = targetValue;
    }

    private void TryJump()
    {
        if(_onGround)
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            EventInvoke(IsJump);
        }
    }

    private void Move()
    {
        Vector2 moveInput =  _inputController.Player.Move.ReadValue<Vector2>();

        if (moveInput != Vector2.zero)
        {
            Vector3 moveDirection = Quaternion.Euler(0, transform.eulerAngles.y,0) * new Vector3(moveInput.x, 0, moveInput.y);
            _rigidBody.velocity += moveDirection * _targetSpeedMove * Time.deltaTime;

            if(_rigidBody.velocity.magnitude >= _targetSpeedMove && _onGround)
            _rigidBody.velocity = _rigidBody.velocity.normalized * _targetSpeedMove;

            if (_rigidBody.velocity.magnitude <= _minSpeed && _onGround)
            _rigidBody.velocity = _rigidBody.velocity.normalized * _minSpeed;
        }

    }

    private void TurnHead()
    {
        Vector2 targetMouseDelta = _inputController.Player.Turn.ReadValue<Vector2>() * Time.smoothDeltaTime;

        if (targetMouseDelta != Vector2.zero)
        {
            _rotation.y += targetMouseDelta.x * _turnSpeed * Time.deltaTime;
            _rotation.x = Mathf.Clamp(_rotation.x - targetMouseDelta.y * _turnSpeed * Time.deltaTime, -90, 90);
            transform.localEulerAngles = new Vector3(transform.eulerAngles.x,_rotation.y,transform.eulerAngles.z);
            _head.transform.localEulerAngles = new Vector3(_rotation.x, 0, transform.eulerAngles.z);
        }
    }

    private void SitDown()
    {
        transform.localScale = _sizeInSquat;
    }

    public PlayerBottom IssueBottom()  
    {
        return _bottom;
    }
}