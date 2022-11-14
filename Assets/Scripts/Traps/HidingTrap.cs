using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingTrap : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targets = new List<GameObject>();
    [SerializeField] private float _speed;
    private float _targetPositionY = -4;
    private bool _isStart = false;
    private GameObject _targetObject;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.name == "Player" && !_isStart)
        {
            _isStart = true;
        }
    }

    private void Start() 
    {
        PickAnObject();
    }

    private void Update() 
    {
        if(_isStart && _targetObject != null && _targetObject.transform.position.y != _targetPositionY)
        {
            _targetObject.transform.position = Vector3.MoveTowards(_targetObject.transform.position, new Vector3(_targetObject.transform.position.x,_targetPositionY,_targetObject.transform.position.z),_speed*Time.deltaTime);

            if(_targetObject.transform.position.y == _targetPositionY)
            {
                _targets.Remove(_targetObject);
                _targetObject = null;
            }
        }
        else if(_targetObject == null || _targetObject.transform.position.y == _targetPositionY)
        PickAnObject();
    }

    private void PickAnObject()
    {
        if(_targets.Count > 0)
        {
            _targetObject = _targets[Random.Range(0, _targets.Count)];
        }
    }
}
