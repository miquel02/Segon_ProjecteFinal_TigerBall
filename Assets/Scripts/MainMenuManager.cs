using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    private ScenesManager scenesManagerScript;

    public bool gameStarted;
   

    // Start is called before the first frame update
    void Start()
    {
        scenesManagerScript = GameObject.Find("Main Menu Manager").GetComponent<ScenesManager>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        DataPersistance.PlayerStats.currentLevel = 1;
        DataPersistance.PlayerStats.currentBombs = 5;
        DataPersistance.PlayerStats.currentLives = 10;
        scenesManagerScript.StartGame();
        gameStarted = true;
    }
}
