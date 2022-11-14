using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Button _start;
    [SerializeField] private Button _finish;

    private float _currentTime = 0;
    private bool _isStarted = false;

    public UnityEngine.Events.UnityEvent RecordsSaved = new UnityEngine.Events.UnityEvent();

    private void Awake() 
    {
        _start.ClickAction.AddListener(ChangeStatus);
        _finish.ClickAction.AddListener(ChangeStatus);
        _finish.ClickAction.AddListener(SaveRecords);
    }

    private void Update() 
    {
        if (_isStarted)
        {
            _currentTime += Time.deltaTime;
            ConvertTime();
        }
    }

    private void ChangeStatus()
    {
        _isStarted = !_isStarted;
    }

    private void ConvertTime()
    {
        int targetSecond;
        int targetMinute;
        string minute;
        string second;

        targetMinute = (int)_currentTime / 60;
        targetSecond = (int)_currentTime % 60;

        if(targetMinute < 10)
        minute = $"0{targetMinute}";
        else
        minute = $"{targetMinute}";

        if(targetSecond < 10)
        second = $"0{targetSecond}";
        else
        second = $"{targetSecond}";

        _timerText.text = $"{minute} : {second}";
    }

    private void SaveRecords()
    {
        if(!PlayerPrefs.HasKey("record"))
        PlayerPrefs.SetInt("record", (int)_currentTime);
        else if(PlayerPrefs.GetInt("record") > _currentTime)
        PlayerPrefs.SetInt("record", (int)_currentTime);

        if(RecordsSaved != null)
        RecordsSaved.Invoke();
    }
}