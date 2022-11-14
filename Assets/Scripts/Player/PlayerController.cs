using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotionController))]
[RequireComponent(typeof(PlayerAudioController))]
public class PlayerController : MonoBehaviour
{
    private Vector3 _startPosition;
    private PlayerMotionController _mover;
    private PlayerAudioController _audioController;

    private void Awake() 
    {
        _mover = GetComponent<PlayerMotionController>();
        _audioController = GetComponent<PlayerAudioController>();
    }

    private void Start() 
    {
        _startPosition = transform.position;
        _mover.IssueBottom().WentToLava.AddListener(TPPlayer);
        _mover.IsSteps.AddListener(_audioController.PlaySteps);
        _mover.StopSteps.AddListener(_audioController.StompingStop);
        _mover.IsJump.AddListener(_audioController.PlayJump);
    }

    private void TPPlayer()
    {
        transform.position = _startPosition;
    }
}