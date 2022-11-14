using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    private PlayerMotionController _tempPlayer;
    private Vector3 _buttonStartPosition;
    private float _puttonOffset = 0.05f;
    private float _speed = 0.1f;
    private bool _isPressed = false;

    public UnityEvent ClickAction = new UnityEvent();

    private void Awake() 
    {
        _buttonStartPosition = _button.transform.position;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.name == "Player" && _tempPlayer == null && !_isPressed)
        {
            _tempPlayer = other.GetComponent<PlayerMotionController>();
            _tempPlayer.ClickAction.AddListener(TryButtonClick);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.name == "Player" && _tempPlayer != null)
        {
            _tempPlayer.ClickAction.RemoveListener(TryButtonClick);
            _tempPlayer = null;
        }
    }

    private void Update() 
    {
        if(_button.transform.position.y != _buttonStartPosition.y)
        _button.transform.position = Vector3.MoveTowards(_button.transform.position, _buttonStartPosition, _speed*Time.deltaTime);
    }

    private void TryButtonClick()
    {
        if(!_isPressed)
        {
            _button.transform.position = new Vector3(_button.transform.position.x, _button.transform.position.y - _puttonOffset, _button.transform.position.z);
            _tempPlayer.ClickAction.RemoveListener(TryButtonClick);
            _isPressed = true;
            
            if(ClickAction != null)
            ClickAction.Invoke();

            AudioSource tempPlayer = GetComponent<AudioSource>();
            tempPlayer.clip = Resources.Load<AudioClip>("Sounds/Click");
            tempPlayer.Play();
        }
    }
}