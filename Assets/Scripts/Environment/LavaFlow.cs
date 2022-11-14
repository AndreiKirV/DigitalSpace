using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    [SerializeField] private Material _targetMaterial;
    private Vector2 _currentOffset = new Vector2(0,0);
    private Vector2 _targetOffset = new Vector2(0,1);
    private float _speed = 0.001f;
    
    private void FixedUpdate() 
    {
        MoveSurface();
    }

    private void MoveSurface()
    {
        _currentOffset = new Vector2(_currentOffset.x + _speed* _targetOffset.x, _currentOffset.y +_speed* _targetOffset.y);
        _targetMaterial.SetTextureOffset("_MainTex", _currentOffset);
    }
}