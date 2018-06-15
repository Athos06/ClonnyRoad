using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Manager : MonoBehaviour
{
    public UIController uiController;

    public int levelCount = 25;
    public Text coin = null;
    public Text distance = null;
    public Text score = null;

    public Camera gameCamera = null;
    public CameraFollow cameraController;
    
    public LevelGenerator levelGenerator = null;

    private int currentCoins = 0;
    private int currentDistance = 0;
    private bool canPlay = false;


    private int highScore = 0;

    private static Manager s_Instance;
    public static Manager instance
    {
        get
        {
            if(s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Manager)) as Manager;
            }

            return s_Instance;
        }

    }


    private void Start()
    {
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
        Debug.Log("Player picked up another coin for " + value);

        currentCoins += value;

        coin.text = currentCoins.ToString();

        levelGenerator.RandomGenerator();
    }

    public void UpdateDistanceCount()
    {
        Debug.Log("Player moved forward for one point");

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
