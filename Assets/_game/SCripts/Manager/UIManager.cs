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

    public void ShowIntro()
    {
        joystick.SetActive(false);
        introPanel.SetActive(true);
        clickToPlay.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    public void ShowClickToPlay()
    {
        joystick.SetActive(false);
        introPanel.SetActive(false);
        clickToPlay.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    public void HideClickToPlay()
    {
        joystick.SetActive(true);
        introPanel.SetActive(false);
        clickToPlay.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    public void ShowWinPanel()
    {
        joystick.SetActive(false);
        introPanel.SetActive(false);
        clickToPlay.SetActive(false);
        winPanel.SetActive(true);
        losePanel.SetActive(false);
    }
    public void ShowLosePanel ()
    {
        joystick.SetActive(false);
        introPanel.SetActive(false);
        clickToPlay.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(true);
    }
}
