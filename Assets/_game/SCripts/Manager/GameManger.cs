using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    public bool isWin = false;
    public bool isLose = false;
    private void Awake()
    {
        Instance= this;
    }
    public void StartGame()
    {
        Time.timeScale = 0f;
        UIManager.instance.ShowClickToPlay();
        UIManager.instance.HideJoystick();
    }

    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        if(isWin)
        {
            UIManager.instance.ShowWinPanel();
        }
        else if(isLose)
        {
            UIManager.instance.ShowLosePanel();
        }
    }
    private void OnInit() {
        StartGame();
    }
    /*private void Update()
    {
        if(isWin || isLose)
        {
            Time.timeScale = 0f;
        }
    }*/
    public void ClickToPlay()
    {
        //turn off clicktoplay panel
        UIManager.instance.HideClickToPlay();
        Time.timeScale = 1f;

    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
