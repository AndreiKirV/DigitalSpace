using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LaunchPlatform : MonoBehaviour
{
    [SerializeField]private float _height;
    private float _targetPositionY;
    private float _force = 120f;
    private float _speed = 10;
    private bool _isStart = false;
    private bool _isAcceleration = false;
    private GameObject _player;

    private void OnEnable() 
    {
        _targetPositionY = transform.position.y + _height;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.name == "Player" && !_isStart)
        {
            _player = other.gameObject;
            _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _force * Time.deltaTime, ForceMode.VelocityChange);
            _isStart = true;

            AudioSource tempPlayer = GetComponent<AudioSource>();
            tempPlayer.clip = Resources.Load<AudioClip>("Sounds/LaunchPlatform");
            tempPlayer.Play();
        }
    }
    
    private void Update() 
    {
        if(_isStart && !_isAcceleration)
        {
           transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,  _targetPositionY, transform.position.z), _speed*Time.deltaTime);
           _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _force * Time.deltaTime, ForceMode.VelocityChange);
        }

        if(Vector3.Distance(transform.position, new Vector3(transform.position.x,  _targetPositionY, transform.position.z)) <= 0.1f && !_isAcceleration)
        {
            _isAcceleration = true;
        }
    }
}