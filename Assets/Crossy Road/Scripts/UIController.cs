using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public CanvasGroup gameUI;
    public CanvasGroup startScreen;
    public CanvasGroup pauseScreen;
    public CanvasGroup gameOverScreen;

    public GameObject HighScoreGameOver;
    public GameObject NormalGameOver;

    public Toggle audioToggle;

    // Use this for initialization
    void Start () {
        setAudioToggle();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            audioToggle.isOn = !audioToggle.isOn;
        }
    }

    private void setAudioToggle()
    {
       
        //has to be the opposite of audioEnabled because when the toggle is on means the cross is on top of the icon, meaning muted
        audioToggle.isOn = !AudioManager.Instance.AudioEnabled;

    }

    public void ToggleChange()
    {
        AudioManager.Instance.ToggleAudio(!audioToggle.isOn);
    }

    public void StartGameButton()
    {
        startScreen.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    public void PauseGameButton()
    {
        Time.timeScale = 0;

        pauseScreen.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }

    public void unpauseGameButton()
    {
        Time.timeScale = 1;

        pauseScreen.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    public void RestartGameButton()
    {
        Time.timeScale = 1;
    }

    public void GameOver(bool HighScore)
    {
        gameOverScreen.gameObject.SetActive(true);

        if (HighScore)
        {
            HighScoreGameOver.SetActive(true);
        }
        else
        {
            NormalGameOver.SetActive(true);
        }

        gameUI.gameObject.SetActive(false);
    }
}
