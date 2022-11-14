using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameOverPanel;

    private void Awake() 
    {
        OpenStartPanel();
    }

    private void ChangeStatus(GameObject targetObject)
    {
        targetObject.SetActive(!targetObject.activeSelf);
    }

    public void OpenStartPanel()
    {
        if(!_startPanel.activeSelf)
        ChangeStatus(_startPanel);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseStartPanel()
    {
        if(_startPanel.activeSelf)
        ChangeStatus(_startPanel);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit()
    {
        if(PlayerPrefs.HasKey("record"))
        PlayerPrefs.DeleteKey("record");
        
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenGameOverPanel()
    {
        if(!_gameOverPanel.activeSelf)
        ChangeStatus(_gameOverPanel);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
}