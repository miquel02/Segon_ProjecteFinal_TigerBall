using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_ScenesManager : MonoBehaviour
{

    public void NextLevel(int Level)
    {
        SceneManager.LoadScene($"MG_Level_{Level}");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MG_Level_1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MG_MainMenu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MG_GameOver");
    }
    public void GameVictory()
    {
        SceneManager.LoadScene("MG_GameWin");
    }
}
