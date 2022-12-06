using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_ScenesManager : MonoBehaviour
{
    //Script to manage scenes
    public void NextLevel(int Level)
    {
        SceneManager.LoadScene($"MG_Level_{Level}");//Function to acces the next level
    }

    public void StartGame()//Function to start the game
    {
        SceneManager.LoadScene("MG_Level_1");
        MG_DataPersistance.PlayerStats.currentLevel = 1;//Set the correct values
        MG_DataPersistance.PlayerStats.currentBombs = 5;//Set the correct values
        MG_DataPersistance.PlayerStats.currentLives = 10;//Set the correct values
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MG_MainMenu");//Load main menu scene
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MG_GameOver");//Load game over scene
    }
    public void GameVictory()
    {
        SceneManager.LoadScene("MG_GameWin");//Load game win scene
    }
}
