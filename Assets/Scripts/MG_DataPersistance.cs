using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DataPersistance : MonoBehaviour
{
    //Script to keep values between scenes
    public int currentLevel;
    public int currentBombs;
    public int currentLives;

    public static MG_DataPersistance PlayerStats;

    void Awake()
    {
        if (PlayerStats == null)
        {
            PlayerStats = this;
            DontDestroyOnLoad(PlayerStats);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //We get the needed variables
        currentLevel = PlayerPrefs.GetInt("LEVELS");
        currentBombs = PlayerPrefs.GetInt("BOMBS");
        currentLives = PlayerPrefs.GetInt("LIVES");
    }

    public void SaveStats()
    {
        //We set the variables
        PlayerPrefs.SetInt("LEVELS", currentLevel);
        PlayerPrefs.SetInt("BOMBS", currentBombs);
        PlayerPrefs.SetInt("LIVES", currentLives);
    }
}
