using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MG_MainMenuManager : MonoBehaviour
{

    private MG_ScenesManager scenesManagerScript;

    public bool gameStarted;
   

    // Start is called before the first frame update
    void Start()
    {
        scenesManagerScript = GameObject.Find("Main Menu Manager").GetComponent<MG_ScenesManager>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        /*MG_DataPersistance.PlayerStats.currentLevel = 1;
        MG_DataPersistance.PlayerStats.currentBombs = 5;
        MG_DataPersistance.PlayerStats.currentLives = 10;*/
        scenesManagerScript.StartGame();
        gameStarted = true;
    }
}
