using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    private AudioSource _audioPlayer;
    private Dictionary<string, AudioClip> _sounds = new Dictionary<string, AudioClip>();

    private void Awake() 
    {
        _audioPlayer = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        LoadSounds();
    }

    private void LoadSounds()
    {
        _sounds.Add("Stomping", Resources.Load<AudioClip>("Sounds/Stomping"));
        _sounds.Add("Jump", Resources.Load<AudioClip>("Sounds/Jump"));
    }

    public void PlayJump()
    {
        _audioPlayer.loop = false;
        _audioPlayer.clip = _sounds["Jump"];
        _audioPlayer.Play();
    }

    public void PlaySteps()
    {
        if(!_audioPlayer.isPlaying)
        {
            _audioPlayer.loop = false;
            _audioPlayer.clip = _sounds["Stomping"];
            _audioPlayer.Play();
        }
    }

    public void StompingStop()
    {
        if(_audioPlayer.clip == _sounds["Stomping"] && _audioPlayer.isPlaying)
        _audioPlayer.Stop();
    }
}