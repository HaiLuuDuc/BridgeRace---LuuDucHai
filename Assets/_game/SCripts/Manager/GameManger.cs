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
    [SerializeField] public UIManager uiManager;
    IEnumerator StartGame()
    {
        uiManager.ShowIntro();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        uiManager.ShowClickToPlay();
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        StartCoroutine(StartGame());
    }
    public void ClickToPlay()
    {
        //turn off clicktoplay panel
        uiManager.HideClickToPlay();
        Time.timeScale = 1f;

    }
    public void Replay()
    {
        //reload prefab
    }
    public void Next()
    {
        //change prefab
    }
}
