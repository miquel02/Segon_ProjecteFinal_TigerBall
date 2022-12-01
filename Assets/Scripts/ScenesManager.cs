using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public void NextLevel(int Level)
    {
        SceneManager.LoadScene($"Level_{Level}");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }
}
