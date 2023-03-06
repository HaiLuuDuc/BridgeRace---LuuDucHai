using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject joystick;
    [SerializeField] public GameObject introPanel;
    [SerializeField] public GameObject clickToPlay;
    [SerializeField] public GameObject winPanel;
    [SerializeField] public GameObject losePanel;
    public static UIManager instance;
    private void Awake()
    {
        instance= this;
    }
    public void CloseAll()
    {
        joystick.SetActive(false);
        introPanel.SetActive(false);
        clickToPlay.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    public void ShowIntro()
    {
        CloseAll();
        introPanel.SetActive(true);
    }
    public void ShowClickToPlay()
    {
        CloseAll();
        clickToPlay.SetActive(true);
    }
    public void HideClickToPlay()
    {
        CloseAll();
        joystick.SetActive(true);
    }
    public void ShowWinPanel()
    {
        CloseAll();
        winPanel.SetActive(true);
    }
    public void ShowLosePanel ()
    {
        CloseAll();
        losePanel.SetActive(true);
    }
    public void ShowJoystick()
    {
        joystick.SetActive(true);
    }
    public void HideJoystick()
    {
        joystick.SetActive(false);
    }
}
