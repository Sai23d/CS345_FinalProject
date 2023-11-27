using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public ScoreKeeper scoreKeeper; // Reference to the ScoreKeeper script
    private bool levelLoaded = false; // To prevent loading the level multiple times

    void Update()
    {
        if (!levelLoaded && scoreKeeper != null && scoreKeeper.GetScore() >= 1000)
        {
            LoadNextLevel();
            levelLoaded = true;
        }
    }

    private void LoadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Game") // Assuming "Game" or "LevelOne" is your first level
        {
            SceneManager.LoadScene("LevelTwo");
        }
        else if (currentScene == "LevelTwo")
        {
            SceneManager.LoadScene("LevelThree");
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the Game...");
        Application.Quit();
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("LevelThree");
    }


    public void CheckLevelProgress()
    {
        if (!levelLoaded && scoreKeeper != null && scoreKeeper.GetScore() >= 1000)
        {
            LoadNextLevel();
            levelLoaded = true;
        }
    }

}
