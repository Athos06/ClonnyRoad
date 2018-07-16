using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Manager : Singleton<Manager>
{
    private UIController uiController;
    public UIController uiControllerPortraitMode;
    public UIController uiControllerLandscapeMode;

    [Header("GUI texts")]    
    private Text coin = null;
    private Text distance = null;
    private Text score = null;
    [Header("GUI Portrait mode texts")]
    public Text coinPortrait = null;
    public Text distancePortrait = null;
    public Text scorePortrait = null;
    [Header("GUI Landscape mode texts")]
    public Text coinLandscape = null;
    public Text distanceLandscape = null;
    public Text scoreLandscape = null;

    [Header("Manager set up")]
    [SerializeField]
    private int levelCount = 25;

    public Camera gameCamera = null;
    public CameraFollow cameraController;
    public LevelGenerator levelGenerator = null;

    private int currentCoins = 0;
    private int currentDistance = 0;
    private bool canPlay = false;


    private int highScore = 0;

    private void Start()
    {
        if(Camera.main.aspect < 1.0f)
        {
            uiController = uiControllerPortraitMode;

            coin = coinPortrait;
            distance = distancePortrait;
            score = scorePortrait;
        }
        else
        {
            uiController = uiControllerLandscapeMode;

            coin = coinLandscape;
            distance = distanceLandscape;
            score = scoreLandscape;
        }

        for (int i = 0; i < levelCount; i++ )
        {
            levelGenerator.RandomGenerator();
        }

        coin.text = "0";
        distance.text = "0";

        LoadHighScore();

    }
   

    public void UpdateCoinCount(int value)
    {
        currentCoins += value;

        coin.text = currentCoins.ToString();

        levelGenerator.RandomGenerator();
    }

    public void UpdateDistanceCount()
    {

        currentDistance += 1;

        distance.text = currentDistance.ToString();

        levelGenerator.RandomGenerator();
        levelGenerator.RemovedOldGround();
    }

    public bool CanPlay()
    {
        return canPlay;
    }

    public void StartPlay()
    {
        canPlay = true;
    }

    public void GameOver()
    {
        gameCamera.GetComponent<CameraShake>().Shake();
        cameraController.enabled = false;

        score.text = "Score: " + currentDistance;

        bool record = false;

        if (currentDistance > highScore)
        {
            SaveHighScore();
            record = true;
        }

        uiController.GameOver(record);


    }



    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(Saves.HighScore);
    }

    private void SaveHighScore()
    {
        highScore = currentDistance;
        PlayerPrefs.SetInt(Saves.HighScore, highScore);
    }

    private void IsFirstTimePlayer()
    {
        if (!PlayerPrefs.HasKey(Saves.FirstTimePlayed))
        {
            PlayerPrefs.SetInt(Saves.FirstTimePlayed, 1);
            PlayerPrefs.SetInt(Saves.MusicOn, 1);
            PlayerPrefs.SetInt(Saves.HighScore, 0);
        }
    }
}
