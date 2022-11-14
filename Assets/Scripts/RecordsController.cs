using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecordsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recordsText;
    [SerializeField] private Timer _target;
    private string _defolt = "Последний рекорд: ";
    private void Awake() 
    {
        SetRecords();
        _target.RecordsSaved.AddListener(SetRecords);
    }

    private void SetRecords()
    {
        if(PlayerPrefs.HasKey("record"))
        {
            int targetTime = PlayerPrefs.GetInt("record");
            int targetSecond;
            int targetMinute;

            targetMinute = (int)targetTime / 60;
            targetSecond = (int)targetTime % 60;

            _recordsText.text = $"{_defolt} {targetMinute} минут, {targetSecond} секунд";
        }
        else
        _recordsText.text = $"{_defolt} отсутствует";
    }
}