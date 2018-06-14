using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public CanvasGroup gameUI;
    public CanvasGroup startScreen;
    public CanvasGroup pauseScreen;
    public CanvasGroup gameOverScreen;
    

    // Use this for initialization
    void Start () {
		
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

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }
}
